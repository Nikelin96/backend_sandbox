
using DataAccessLibrary;
using DataAccessLibrary.Models;

namespace DataAccessLibrary.Repositories;
public sealed class KingdomRepository : ICreateEntityCommand<Kingdom>, IRetrieveEntitesQuery<Kingdom>
{
    private readonly IConnectionCreator _connectionCreator;
    private readonly IDataAccessExecutor _executor;

    public KingdomRepository(IConnectionCreator connectionCreator, IDataAccessExecutor executor)
    {
        _connectionCreator = connectionCreator;
        _executor = executor;
    }

    public async Task<int> Create(Kingdom kingdom)
    {
        // INSERT INTO continent (name) VALUES ('Europe');
        var sql = @"INSERT INTO kingdom(name, rank, continent_id) VALUES (@Name, @Rank, @ContinentId) RETURNING id;";

        using var connection = _connectionCreator.Create();

        return await _executor.ExecuteScalarAsync<int>(connection, sql, kingdom);
    }

    public async Task<IEnumerable<Kingdom>> RetrieveEntities()
    {
        using var connection = _connectionCreator.Create();

        var sql = @"SELECT * FROM kingdom;";

        return await _executor.QueryAsync<Kingdom>(connection, sql);
    }
}