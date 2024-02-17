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

        using var connection = _connectionCreator.Create();
        // we are better
        // INSERT INTO kingdom_technology (kingdom_id, technology_id, kingdom_transaction_id, research_status, research_start_time) VALUES(1, 1, 2, 'completed', CURRENT_TIMESTAMP - INTERVAL '30 days');
        var sql = @"INSERT INTO kingdom_technology (kingdom_id, technology_id, kingdom_transaction_id, research_status, research_start_time) 
                    VALUES (@KingdomId, @TechnologyId, @KingdomTransactionId, @ResearchStatus::research_status_type, TO_TIMESTAMP(@ResearchStartTime::text, 'YYYY-MM-DD HH24:MI:SS')) 
                    RETURNING id;";

        return await _executor.ExecuteScalarAsync<int>(connection, sql,
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
