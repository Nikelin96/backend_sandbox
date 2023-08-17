using DataAccessLibrary.Models;

namespace DataAccessLibrary.Repositories;
public sealed class StatRepository : ICreateEntityCommand<Stat>
{
    private readonly IConnectionCreator _connectionCreator;
    private readonly IDataAccessExecutor _executor;

    public StatRepository(IConnectionCreator connectionCreator, IDataAccessExecutor executor)
    {
        _connectionCreator = connectionCreator;
        _executor = executor;
    }

    public async Task<int> Create(Stat stat)
    {
        //INSERT INTO stat (hit_points, defense_points, damage_points, health_points) VALUES (100, 100, 100, 100);
        var sql = @"INSERT INTO stat (hit_points, defense_points, damage_points, health_points) VALUES (@HitPoints, @DefensePoints, @DamagePoints, @HealthPoints) RETURNING id;";

        using var connection = _connectionCreator.Create();

        return await _executor.ExecuteScalarAsync<int>(connection, sql, stat);
    }
}
