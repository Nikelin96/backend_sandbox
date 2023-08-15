namespace GrpcBackendService.DataAccess.Repositories;

using Dapper;
using GrpcBackendService.DataAccess;
using GrpcBackendService.Models;

public sealed class StatRepository : ICreateEntityCommand<Stat>
{
    private DataContext _context;

    public StatRepository(DataContext context)
    {
        _context = context;
    }

    public async Task<int> Create(Stat stat)
    {
        //INSERT INTO stat (hit_points, defense_points, damage_points, health_points) VALUES (100, 100, 100, 100);
        var sql = @"INSERT INTO stat (hit_points, defense_points, damage_points, health_points) VALUES (@HitPoints, @DefensePoints, @DamagePoints, @HealthPoints) RETURNING id;";

        using var connection = _context.CreateConnection();

        return await connection.ExecuteScalarAsync<int>(sql, stat);
    }
}
