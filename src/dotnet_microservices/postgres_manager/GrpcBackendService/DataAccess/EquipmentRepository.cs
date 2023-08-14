using Dapper;
using GrpcBackendService.Helpers;
using GrpcBackendService.Models;

namespace GrpcBackendService.DataAccess;
public class EquipmentRepository : ICreateEntityCommand<Equipment>
{
    private DataContext _context;

    public EquipmentRepository(DataContext context)
    {
        _context = context;
    }

    public async Task<int> Create(Equipment equipment)
    {
        // INSERT INTO equipment (name, stat_id) VALUES ('chain mail', 3); 
        var sql = @"INSERT INTO equipment (name, stat_id) VALUES (@Name, @StatId) RETURNING id;";

        using var connection = _context.CreateConnection();

        return await connection.ExecuteScalarAsync<int>(sql, equipment);
    }
}
