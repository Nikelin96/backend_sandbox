using DataAccessLibrary;
using DataAccessLibrary.Models;
using DataAccessLibrary.Repositories;

namespace GrpcBackendService.UnitsOfWork;
public sealed class CreateKingdomTechnologyJourney
{


    private readonly KingdomTechnologyRepository _kingdomTechnologyRepository;
    private readonly IConnectionCreator _connectionCreator;
    private readonly IDataAccessExecutor _executor;

    public CreateKingdomTechnologyJourney(KingdomTechnologyRepository kingdomTechnologyRepository
        , IConnectionCreator connectionCreator, IDataAccessExecutor executor)
    {
        _connectionCreator = connectionCreator;
        _executor = executor;


        _kingdomTechnologyRepository = kingdomTechnologyRepository;
    }

    public async Task<int> CreateKingdomTechnology(KingdomTechnology kingdomTecnology)
    {
        // check that technology exist, take its price
        var queryTechnologyPrice = @"SELECT * FROM get_technology_prices(@technology_identifier);";

        using var connection = _connectionCreator.Create();

        var technologiesWithPrice = await _executor.QueryAsync<TechnologyPrice>(connection, queryTechnologyPrice, new { technology_identifier = kingdomTecnology.TechnologyId });

        if (technologiesWithPrice.Count() == 0)
        {
            throw new KeyNotFoundException("Technology with such id does not exist");
        }

        // check that kingdom_transaction exists, and its value equals to technology price
        var queryKingdomTransaction =  @"SELECT * FROM kingdom_transaction WHERE kingdom_id = @KingdomId AND technology_id = @TechnologyId";

        if (kingdomTecnology.UnitId.HasValue)
        {
            queryKingdomTransaction += " AND unit_id = @UnitId";
        }
        else
        {
            queryKingdomTransaction += " AND unit_id IS NULL";
        }
        if (kingdomTecnology.SkillId.HasValue)
        {
            queryKingdomTransaction += " AND skill_id = @SkillId";
        }
        else
        {
            queryKingdomTransaction += " AND skill_id IS NULL";
        }
        if (kingdomTecnology.EquipmentId.HasValue)
        {
            queryKingdomTransaction += " AND equipment_id = @EquipmentId";
        }
        else
        {
            queryKingdomTransaction += " AND equipment_id IS NULL";
        }

        queryKingdomTransaction += ";";

        // todo add conditions AND unit_id = @UnitId AND skill_id = @SkillId AND equipment_id = @EquipmentId;;

        var kingdomTransactions = await _executor.QueryAsync<KingdomTransaction>(connection, queryKingdomTransaction, kingdomTecnology);

        if (kingdomTransactions.Count() > 0)
        { //we are good

            var woodTotal = technologiesWithPrice.Sum((x)=> x.Wood);
            var foodTotal = technologiesWithPrice.Sum((x)=> x.Food);
            var goldTotal = technologiesWithPrice.Sum((x)=> x.Gold);
            var stoneTotal = technologiesWithPrice.Sum((x)=> x.Stone);

            var checkSum = kingdomTransactions.First();

            if (checkSum.Wood == woodTotal && checkSum.Food == foodTotal && checkSum.Gold == goldTotal && checkSum.Stone == stoneTotal)
            {

                return await _kingdomTechnologyRepository.Create(kingdomTecnology);
            }
        }
        // todo write warning that record was not inserted, because transactions were not found
        return -1;
    }
}