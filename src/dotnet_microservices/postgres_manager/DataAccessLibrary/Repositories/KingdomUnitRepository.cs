using DataAccessLibrary.Models;

namespace DataAccessLibrary.Repositories;
public sealed class KingdomUnitRepository
{
    private readonly IConnectionCreator _connectionCreator;
    private readonly IDataAccessExecutor _executor;

    public KingdomUnitRepository(IConnectionCreator connectionCreator, IDataAccessExecutor executor)
    {
        _connectionCreator = connectionCreator;
        _executor = executor;
    }

    public async Task<IEnumerable<KingdomUnit>> RetrieveEntities(int id)
    {
        var query = @"SELECT * FROM get_kingdom_units(@kingdom_identifier);";

        using var connection = _connectionCreator.Create();

        var results = await _executor.QueryAsync<KingdomUnit>(connection, query, new { kingdom_identifier = id });

        return results;
    }
}
