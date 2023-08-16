namespace GrpcBackendService.DataAccess.Repositories;

using GrpcBackendService.DataAccess;
using GrpcBackendService.Models;
//using Npgsql;

public sealed class SkillRepository : ICreateEntityCommand<Skill>
{
    private readonly IConnectionCreator _connectionCreator;
    private readonly IDataAccessExecutor _executor;

    public SkillRepository(IConnectionCreator connectionCreator, IDataAccessExecutor executor)
    {
        _connectionCreator = connectionCreator;
        _executor = executor;
    }

    public async Task<int> Create(Skill skill)
    {
        //INSERT INTO skill(type, stat_id) VALUES ('attack', 1);
        var sql = @"INSERT INTO skill(type, stat_id) VALUES (@Type::skill_type, @StatId) RETURNING id;";

        using var connection = _connectionCreator.Create();

        return await _executor.ExecuteScalarAsync<int>(connection, sql, new { Type = skill.Type.ToString().ToLower(), skill.StatId });


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

