using DataAccessLibrary;
using DataAccessLibrary.Models;
using DataAccessLibrary.Repositories;

namespace GrpcBackendService.UnitsOfWork;
public sealed class CreateKingdomUnitJourney
{
    private readonly StatRepository _statRepository;
    private readonly PriceRepository _priceRepository;
    private readonly UnitRepository _unitRepository;
    private readonly KingdomUnitRepository _kingdomUnitRepository;
    private readonly IConnectionCreator _connectionCreator;
    private readonly IDataAccessExecutor _executor;

    public CreateKingdomUnitJourney(StatRepository statRepository, PriceRepository priceRepository, UnitRepository unitRepository, KingdomUnitRepository kingdomUnitRepository, IConnectionCreator connectionCreator, IDataAccessExecutor executor)
    {
        _statRepository = statRepository;
        _priceRepository = priceRepository;
        _unitRepository = unitRepository;
        _kingdomUnitRepository = kingdomUnitRepository;
        _connectionCreator = connectionCreator;
        _executor = executor;
    }

    public async Task<int> CreateKingdomUnit(KingdomUnit unit)
    {
        try
        {
            using var connection = _connectionCreator.Create();

            // throws exception when no elements, and when > 1
            var kingdomQuery = @"SELECT * FROM kingdom WHERE id = @KingdomId;";
            var checkIfKingdomExists = await _executor.QuerySingleAsync<Kingdom>(connection, kingdomQuery, new { KingdomId = unit.KingdomId });

            var getUnitTechnologiesQuery = @"SELECT * FROM get_unit_technologies(@unit_identifier);";
            var unitTechnologies = await _executor.QueryAsync<UnitTechnology>(connection, getUnitTechnologiesQuery, new { unit_identifier = unit.UnitId });

            if (unitTechnologies.Count() == 0)
            {
                throw new ArgumentException($"no technologies found for unit with id: ${unit.UnitId}");
            }

            var equipmentIds = unitTechnologies.Where(x => x.EquipmentId.HasValue).Select(x => x.EquipmentId).ToArray();
            var skillIds = unitTechnologies.Where(x => x.SkillId.HasValue).Select(x => x.SkillId).ToArray();

            var queries = @"
            SELECT * FROM price WHERE unit_id = @unitId;
            SELECT * FROM price WHERE equipment_id = ANY (@equipmentIds);
            SELECT * FROM price WHERE skill_id = ANY (@skillIds);
        ";

            var pricesQueryResult = await _executor.QueryMultiple(connection,queries, new { unitId = unit.UnitId, equipmentIds = equipmentIds, skillIds = skillIds } );

            var unitPrices = pricesQueryResult.Read<Price>();
            var equipmentPrices = pricesQueryResult.Read<Price>();
            var skillPrices = pricesQueryResult.Read<Price>();

            var allPrices = unitPrices.Concat(equipmentPrices).Concat(skillPrices);

            var totalWood = allPrices.Sum(x => x.Wood);
            var totalFood = allPrices.Sum(x => x.Food);
            var totalGold = allPrices.Sum(x => x.Gold);
            var totalStone = allPrices.Sum(x => x.Stone);


            var transactionQueries = @"
            SELECT * FROM kingdom_transaction WHERE kingdom_id = @KingdomId and unit_id = @UnitId;
            SELECT * FROM kingdom_transaction WHERE kingdom_id = @KingdomId and unit_id = @UnitId;
            SELECT * FROM price WHERE equipment_id = ANY (@equipmentIds);
            SELECT * FROM price WHERE skill_id = ANY (@skillIds);
        ";

            //var pricesQueryResult = await _executor.QueryMultiple(connection,queries, new { unitId = unit.UnitId, equipmentIds = equipmentIds, skillIds = skillIds } );

            //var unitPrices = pricesQueryResult.Read<Price>();
            //var equipmentPrices = pricesQueryResult.Read<Price>();
            //var skillPrices = pricesQueryResult.Read<Price>();

            // get all related technologies for the unit(required+optional)
            // check that kingdom has all required technologies for that unit   
            // check that kingdom has transaction related to unit purchase, price matches to everything with previous step
            // here we are good
            return 1;
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
            throw;
        }
    }
}
