using Dapper;
using GrpcBackendService.Helpers;
using GrpcBackendService.Models;

namespace GrpcBackendService.DataAccess;

public sealed class KingdomRepository : IDataRepository<Kingdom>
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

    public Task Delete(int id)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<Kingdom>> GetAll()
    {
        throw new NotImplementedException();
    }

    public Task<Kingdom> GetById(int id)
    {
        throw new NotImplementedException();
    }

    public Task Update(Kingdom user)
    {
        throw new NotImplementedException();
    }
}
