using DataAccessLibrary.Models;

namespace DataAccessLibrary.Repositories;
public sealed class KingdomTechnologyRepository
{
    private readonly IConnectionCreator _connectionCreator;
    private readonly IDataAccessExecutor _executor;

    public KingdomTechnologyRepository(IConnectionCreator connectionCreator, IDataAccessExecutor executor)
    {
        _connectionCreator = connectionCreator;
        _executor = executor;
    }

    public async Task<IEnumerable<KingdomTechnology>> RetrieveEntities(int id)
    {
        var query = @"SELECT * FROM get_kingdom_technologies(@kingdom_identifier);";

        using var connection = _connectionCreator.Create();

        var results = await _executor.QueryAsync<KingdomTechnology>(connection, query, new { kingdom_identifier = id });

        return results;
    }

    public async Task<int> Create(KingdomTechnology kingdomTecnology)
    {
        // check that technology exist, take its price
        var queryTechnologyPrice = @"SELECT * FROM get_technology_price(@technology_identifier);";

        using var connection = _connectionCreator.Create();

        var technologiesWithPrice = await _executor.QueryAsync<TechnologyPrice>(connection, queryTechnologyPrice, new { technology_identifier = kingdomTecnology.TechnologyId });

        if (technologiesWithPrice.Count() == 0)
        {
            // problem, no such technology
        }
        else if (technologiesWithPrice.Count() > 1)
        {
            // consistensy issue
        }

        var technologyWithPrice = technologiesWithPrice.First();

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

            var checkSum = kingdomTransactions.First();

            if (checkSum.Wood == technologyWithPrice.Wood && checkSum.Food == technologyWithPrice.Food && checkSum.Gold == technologyWithPrice.Gold && checkSum.Stone == technologyWithPrice.Stone)
            {
                // we are better

                // INSERT INTO kingdom_technology (kingdom_id, technology_id, kingdom_transaction_id, research_status, research_start_time) VALUES(1, 1, 2, 'completed', CURRENT_TIMESTAMP - INTERVAL '30 days');
                var sql = @"INSERT INTO kingdom_technology (kingdom_id, technology_id, kingdom_transaction_id, research_status, research_start_time) 
                    VALUES (@KingdomId, @TechnologyId, @KingdomTransactionId, @ResearchStatus::research_status_type, TO_TIMESTAMP(@ResearchStartTime::text, 'YYYY-MM-DD HH24:MI')) RETURNING id;"; //`

                var s = await _executor.ExecuteScalarAsync<int>(connection, sql,
                    new
                    {
                        KingdomId = kingdomTecnology.KingdomId,
                        TechnologyId = kingdomTecnology.TechnologyId,
                        KingdomTransactionId = kingdomTecnology.KingdomTransactionId,
                        ResearchStatus = kingdomTecnology.ResearchStatus.ToPostgreEnum(),
                        ResearchStartTime = kingdomTecnology.ResearchStartTime.ToUniversalTime().ToString("yyyy-MM-dd HH:mm:ss")
                    });
            }
        }

        return -1;
    }
}
