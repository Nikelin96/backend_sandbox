namespace GrpcBackendService.DataAccess.Repositories;

using GrpcBackendService.DataAccess;
using GrpcBackendService.Models;

public sealed class UnitRepository : ICreateEntityCommand<Unit>
{
    private readonly IConnectionCreator _context;
    private readonly IDataAccessExecutor _executor;

    public UnitRepository(IConnectionCreator connectionCreator, IDataAccessExecutor repository)
    {
        _context = connectionCreator;
        _executor = repository;
    }

    public async Task<int> Create(Unit unit)
    {
        // INSERT INTO unit (name, stat_id, kingdom_id) VALUES ('Spearmen', 1, 1);
        var sql = @"INSERT INTO unit (name, stat_id, kingdom_id) VALUES (@Name, @StatId, @KingdomId) RETURNING id;";

        using var connection = _context.Create();

        return await _executor.ExecuteScalarAsync<int>(connection, sql, unit);
    }
}

