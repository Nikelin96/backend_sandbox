namespace GrpcBackendService.DataAccess.Repositories;

using GrpcBackendService.DataAccess;
using GrpcBackendService.Models;

public sealed class PriceRepository : ICreateEntityCommand<Price>
{
    private readonly IConnectionCreator _connectionCreator;
    private readonly IDataAccessExecutor _executor;

    public PriceRepository(IConnectionCreator connectionCreator, IDataAccessExecutor executor)
    {
        _connectionCreator = connectionCreator;
        _executor = executor;
    }

    public async Task<int> Create(Price price)
    {
        // INSERT INTO price(wood, food, gold, stone, technology_id, unit_id, skill_id, equipment_id) VALUES (4, 4, 4, 4, null, null, 1, null);
        var sql = @"INSERT INTO price(wood, food, gold, stone, technology_id, unit_id, skill_id, equipment_id) VALUES (@Wood, @Food, @Gold, @Stone, @TechnologyId, @UnitId, @SkillId, @EquipmentId) RETURNING id;";

        using var connection = _connectionCreator.Create();

        return await _executor.ExecuteScalarAsync<int>(connection, sql, price);
    }
}
