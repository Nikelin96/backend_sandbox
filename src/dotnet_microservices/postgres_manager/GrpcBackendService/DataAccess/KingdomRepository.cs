namespace GrpcBackendService.DataAccess;

using Dapper;
using GrpcBackendService.Helpers;
using GrpcBackendService.Models;

public sealed class KingdomRepository : IDataRepository<Kingdom>
{
    private DataContext _context;

    public KingdomRepository(DataContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Kingdom>> GetAll()
    {
        using var connection = _context.CreateConnection();

        var sql = @"SELECT * FROM kingdom;";

        return await connection.QueryAsync<Kingdom>(sql);
    }

    public async Task<Kingdom> GetById(int id)
    {
        var query = @"SELECT * FROM get_kingdom_technologies(@kingdom_identifier);";

        using var connection = _context.CreateConnection();

        try
        {
            var results = await connection.QueryAsync<Kingdom>(query, new { kingdom_identifier = id });

            return null;

        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
        }

        return null;

    }

    public Task Create(Kingdom user)
    {
        throw new NotImplementedException();
    }

    public Task Update(Kingdom user)
    {
        throw new NotImplementedException();
    }

    public Task Delete(int id)
    {
        throw new NotImplementedException();
    }

    //public async Task<Kingdom> GetByIdWithTechnologies(int id)
    //{
    //    var query = @"
    //         SELECT * FROM kingdom WHERE id = @kingdomId;
    //         SELECT * FROM kingdom_technology WHERE kingdom_id = @kingdomId;
    //        ";

    //    using var connection = _context.CreateConnection();

    //    try
    //    {
    //        var results = await connection.QueryMultipleAsync(query, new { @kingdomId = id });

    //        var kingdom = results.ReadSingle<Kingdom>();
    //        var kingdomTechnologies = results.Read<Technology>();

    //        //kingdom.Technologies.AddRange(kingdomTechnologies);

    //        return kingdom;

    //    }
    //    catch (Exception ex)
    //    {
    //        Console.WriteLine(ex);
    //    }

    //    return null;
    //}

}
