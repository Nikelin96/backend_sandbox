using DataAccessLibrary.Models;

namespace DataAccessLibrary.Repositories;
public sealed class UnitEquipmentRepository
{
    private readonly IConnectionCreator _connectionCreator;
    private readonly IDataAccessExecutor _executor;

    public UnitEquipmentRepository(IConnectionCreator connectionCreator, IDataAccessExecutor executor)
    {
        _connectionCreator = connectionCreator;
        _executor = executor;
    }

    public async Task<int> Create(UnitEquipment unitEquipment)
    {
        // INSERT INTO unit_equipment (equipment_id, unit_id) VALUES (1, 1);
        var sql = @"INSERT INTO unit_equipment (equipment_id, unit_id) VALUES (@EquipmentId, @UnitId) RETURNING id;";

        using var connection = _connectionCreator.Create();

        return await _executor.ExecuteScalarAsync<int>(connection, sql, unitEquipment);
    }
}
