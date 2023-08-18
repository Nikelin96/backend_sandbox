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

    public async Task<int> Create(KingdomUnit unit)
    {
        //var s  UnitTechnology
        var query = @"SELECT * FROM get_unit_technologies(@unit_identifier);";

        using var connection = _connectionCreator.Create();

        var results = await _executor.QueryAsync<UnitTechnology>(connection, query, new { unit_identifier = unit.UnitId });


        // assuming that kingdom exists
        // get all related technologies for the unit(required+optional)
        // check that kingdom has all required technologies for that unit
        // get unit and all related stats, skills, equipment, prices
        // check that kingdom has transaction related to unit purchase, price matches to everything with previous step
        // here we are good
        return 1;
    }
}
