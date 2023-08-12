namespace GrpcBackendService.DataAccess;

using Dapper;
using GrpcBackendService.Helpers;
using GrpcBackendService.Models;

public sealed class KingdomTechnologyRepository : IRetrieveEntitesByIdQuery<KingdomTechnology>
{
    private DataContext _context;

    public KingdomTechnologyRepository(DataContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<KingdomTechnology>> RetrieveEntities(int id)
    {
        var query = @"SELECT * FROM get_kingdom_technologies(@kingdom_identifier);";

        using var connection = _context.CreateConnection();

        try
        {
            var results = await connection.QueryAsync<KingdomTechnology>(query, new { kingdom_identifier = id });

            return results;
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
        }

        return null;
    }
}
