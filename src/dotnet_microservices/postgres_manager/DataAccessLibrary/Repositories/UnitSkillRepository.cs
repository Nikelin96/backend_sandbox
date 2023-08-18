using DataAccessLibrary.Models;

namespace DataAccessLibrary.Repositories;
public sealed class UnitSkillRepository
{
    private readonly IConnectionCreator _connectionCreator;
    private readonly IDataAccessExecutor _executor;

    public UnitSkillRepository(IConnectionCreator connectionCreator, IDataAccessExecutor executor)
    {
        _connectionCreator = connectionCreator;
        _executor = executor;
    }

    public async Task<int> Create(UnitSkill skill)
    {
        // INSERT INTO unit_skill (skill_id, unit_id) VALUES (1, 1);
        var sql = @"INSERT INTO unit_skill (skill_id, unit_id) VALUES (@SkillId, @UnitId) RETURNING id;";

        using var connection = _connectionCreator.Create();

        return await _executor.ExecuteScalarAsync<int>(connection, sql, skill);
    }
}
