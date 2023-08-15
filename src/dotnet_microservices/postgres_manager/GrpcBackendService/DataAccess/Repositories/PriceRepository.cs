namespace GrpcBackendService.DataAccess.Repositories;

using Dapper;
using GrpcBackendService.DataAccess;
using GrpcBackendService.Models;

public sealed class PriceRepository : ICreateEntityCommand<Price>
{
    private DataContext _context;

    public PriceRepository(DataContext context)
    {
        _context = context;
    }

    public async Task<int> Create(Price price)
    {
        // INSERT INTO price(wood, food, gold, stone, technology_id, unit_id, skill_id, equipment_id) VALUES (4, 4, 4, 4, null, null, 1, null);
        var sql = @"INSERT INTO price(wood, food, gold, stone, technology_id, unit_id, skill_id, equipment_id) VALUES (@Wood, @Food, @Gold, @Stone, @TechnologyId, @UnitId, @SkillId, @EquipmentId) RETURNING id;";

        using var connection = _context.CreateConnection();

        return await connection.ExecuteScalarAsync<int>(sql, price);
    }
}
