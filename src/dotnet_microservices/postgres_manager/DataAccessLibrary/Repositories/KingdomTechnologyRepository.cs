
using DataAccessLibrary;
using DataAccessLibrary.Models;

namespace DataAccessLibrary.Repositories;
public sealed class KingdomTechnologyRepository : IRetrieveEntitesByIdQuery<KingdomTechnology>
{
    private readonly IConnectionCreator _context;
    private readonly IDataAccessExecutor _executor;

    public KingdomTechnologyRepository(IConnectionCreator connectionCreator, IDataAccessExecutor executor)
    {
        _context = connectionCreator;
        _executor = executor;
    }

    public async Task<IEnumerable<KingdomTechnology>> RetrieveEntities(int id)
    {
        var query = @"SELECT * FROM get_kingdom_technologies(@kingdom_identifier);";

        using var connection = _context.Create();

        var results = await _executor.QueryAsync<KingdomTechnology>(connection, query, new { kingdom_identifier = id });

        return results;
    }
}
