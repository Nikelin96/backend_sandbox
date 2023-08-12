namespace GrpcBackendService.DataAccess;

using Dapper;
using GrpcBackendService.Helpers;
using GrpcBackendService.Models;

public sealed class KingdomTechnologyRepository : IDataRepository<KingdomTechnology>
{
    private DataContext _context;

    public KingdomTechnologyRepository(DataContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<KingdomTechnology>> GetAll()
    {
        using var connection = _context.CreateConnection();

        var sql = @"SELECT * FROM kingdom;";

        return await connection.QueryAsync<KingdomTechnology>(sql);
    }

    public async Task<KingdomTechnology> GetKingdomTechnologies(int id)
    {
        var query = @"SELECT * FROM get_kingdom_technologies(@kingdom_identifier);";

        using var connection = _context.CreateConnection();

        try
        {
            var results = await connection.QueryAsync<KingdomTechnology>(query, new { kingdom_identifier = id });

            return null;
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
        }

        return null;
    }

    public async Task<KingdomTechnology> GetById(int id)
    {
        throw new NotImplementedException(); ;
    }

    public async Task Create(KingdomTechnology kingdom)
    {
        var sql = @"INSERT INTO kingdom(name, rank, continent_id) values (@name, @rank, @continent_id);";

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

    public Task Update(KingdomTechnology user)
    {
        throw new NotImplementedException();
    }

    public Task Delete(int id)
    {
        throw new NotImplementedException();
    }
}
