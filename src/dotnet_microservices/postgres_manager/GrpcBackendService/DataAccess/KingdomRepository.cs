using Dapper;
using GrpcBackendService.Helpers;
using GrpcBackendService.Models;

namespace GrpcBackendService.DataAccess;

public sealed class KingdomRepository : ICreateEntityCommand<Kingdom>, IRetrieveEntitesQuery<Kingdom>
{
    private DataContext _context;

    public KingdomRepository(DataContext context)
    {
        _context = context;
    }

    public async Task<int> Create(Kingdom kingdom)
    {
        // INSERT INTO continent (name) VALUES ('Europe');
        var sql = @"INSERT INTO kingdom(name, rank, continent_id) VALUES (@Name, @Rank, @ContinentId) RETURNING id;";

        using var connection = _context.CreateConnection();

        return await connection.ExecuteScalarAsync<int>(sql, kingdom);
    }

    public async Task<IEnumerable<Kingdom>> RetrieveEntities()
    {
        using var connection = _context.CreateConnection();

        var sql = @"SELECT * FROM kingdom;";

        return await connection.QueryAsync<Kingdom>(sql);
    }
}