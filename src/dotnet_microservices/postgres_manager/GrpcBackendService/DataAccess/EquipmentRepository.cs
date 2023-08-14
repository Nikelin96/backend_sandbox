//using Dapper;
//using GrpcBackendService.Helpers;
//using GrpcBackendService.Models;

//namespace GrpcBackendService.DataAccess;
//public class EquipmentRepository : ICreateEntityCommand<Technology>
//{
//    private DataContext _context;

//    public EquipmentRepository(DataContext context)
//    {
//        _context = context;
//    }

//    public async Task<int> Create(Technology technology)
//    {
//        //INSERT INTO technology (name, description, research_time) VALUES ('chain mail', 'A technology for chain mail', 50);
//        var sql = @"INSERT INTO technology(name, description, research_time) values (@Name, @Description, @ResearchTime);";

//        using var connection = _context.CreateConnection();

//        await connection.ExecuteAsync(sql, technology);
//    }
//}
