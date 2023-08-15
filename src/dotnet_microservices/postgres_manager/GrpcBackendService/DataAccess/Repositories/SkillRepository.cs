namespace GrpcBackendService.DataAccess.Repositories;

using Dapper;
using GrpcBackendService.DataAccess;
using GrpcBackendService.Models;
//using Npgsql;

public sealed class SkillRepository : ICreateEntityCommand<Skill>
{
    private DataContext _context;

    public SkillRepository(DataContext context)
    {
        _context = context;
    }

    public async Task<int> Create(Skill skill)
    {
        //INSERT INTO skill(type, stat_id) VALUES ('attack', 1);
        var sql = @"INSERT INTO skill(type, stat_id) VALUES (@Type::skill_type, @StatId) RETURNING id;";

        using var connection = _context.CreateConnection();

        return await connection.ExecuteScalarAsync<int>(sql, new { Type = skill.Type.ToString().ToLower(), skill.StatId });


        //using var connection = _context.DataSource.OpenConnection();

        //await using (var cmd = new NpgsqlCommand("INSERT INTO skill(type, stat_id) VALUES ($1, $2) RETURNING id;", connection))
        //{
        //    cmd.Parameters.Add(new() { Value = skill.Type });
        //    cmd.Parameters.Add(new()
        //    {
        //        Value = skill.StatId
        //    });

        //    var recordId = await cmd.ExecuteScalarAsync(new CancellationTokenSource().Token);

        //    return (int)recordId!;
        //}
    }
}

