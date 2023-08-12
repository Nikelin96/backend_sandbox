using Dapper;
using GrpcBackendService.Helpers;
using GrpcBackendService.Models;

namespace GrpcBackendService.DataAccess;

public sealed class TechnologyRepository : ICreateEntityCommand<Technology>
{
    private DataContext _context;

    public TechnologyRepository(DataContext context)
    {
        _context = context;
    }

    public async Task Create(Technology technology)
    {
        //INSERT INTO technology (name, description, research_time) VALUES ('chain mail', 'A technology for chain mail', 50);
        var sql = @"INSERT INTO technology(name, description, research_time) values (@Name, @Description, @ResearchTime);";

        using var connection = _context.CreateConnection();

        try
        {
            await connection.ExecuteAsync(sql, technology);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
        }
    }
}
