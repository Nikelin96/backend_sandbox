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

    public async Task<int> Create(Technology technology)
    {
        //INSERT INTO technology (name, description, research_time) VALUES ('chain mail', 'A technology for chain mail', 50);
        var sql = @"INSERT INTO technology(name, description, research_time) VALUES (@Name, @Description, @ResearchTime) RETURNING id;";

        using var connection = _context.CreateConnection();

        return await connection.ExecuteScalarAsync<int>(sql, technology);
    }
}
