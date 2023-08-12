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

    public async Task Create(Kingdom kingdom)
    {
        var sql = @"INSERT INTO kingdom(name, rank, continent_id) values (@Name, @Rank, @ContinentId);";

        using var connection = _context.CreateConnection();

        try
        {
            await connection.ExecuteAsync(sql, kingdom);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
        }
    }

    public async Task<IEnumerable<Kingdom>> RetrieveEntities()
    {
        using var connection = _context.CreateConnection();

        var sql = @"SELECT * FROM kingdom;";

        return await connection.QueryAsync<Kingdom>(sql);
    }
}