using DataAccessLibrary.Models;
using Npgsql;

namespace DataAccessLibrary.Repositories;
public sealed class TechnologyRepository
{
    private IConnectionCreator _connectionCreator;
    private IDataAccessExecutor _executor;

    public TechnologyRepository(IConnectionCreator connectionCreator, IDataAccessExecutor executor)
    {
        _connectionCreator = connectionCreator;
        _executor = executor;
    }

    public async Task<int> Create(Technology technology)
    {
        //INSERT INTO technology (name, description, research_time) VALUES ('chain mail', 'A technology for chain mail', 50);
        var sql = @"INSERT INTO technology(name, description, research_time) VALUES (@Name, @Description, @ResearchTime) RETURNING id;";

        using var connection = _connectionCreator.Create();

        return await _executor.ExecuteScalarAsync<int>(connection, sql, technology);
    }

    public async Task<IEnumerable<TechnologyPrice>> GetPricesForTechnology(int technologyId)
    {
        var queryTechnologyPrice = @"SELECT * FROM get_technology_prices(@technology_identifier);";

        using var connection = _connectionCreator.Create();

        return await _executor.QueryAsync<TechnologyPrice>(connection, queryTechnologyPrice, new { technology_identifier = technologyId });

    }


    // todo - refactor to extract logic into journey
    public async Task<int> SetTechnologyDependency(TechnologyDependency newTechnologyDependency)
    {
        var query = @"SELECT * FROM get_technology_dependencies(@technology_identifier);";

        using var connection = _connectionCreator.Create();

        var technologyDependencies = await _executor.QueryAsync<TechnologyDependency>(connection, query, new { technology_identifier = newTechnologyDependency.TechnologyId });

        if (technologyDependencies.Count() == 0)
        {
            throw new InvalidOperationException("Technology does not exist");
        }

        var existingDependency = technologyDependencies.FirstOrDefault(x => x.UnitId == newTechnologyDependency.UnitId && x.SkillId == newTechnologyDependency.SkillId && x.EquipmentId == newTechnologyDependency.EquipmentId);
        if (existingDependency != null)
        {
            // such dependency already exists, update only isRequired if needed

            if (existingDependency.IsRequired != newTechnologyDependency.IsRequired)
            {
                var updateQuery = @"UPDATE technology_dependency SET is_required = @isRequired WHERE id = @id RETURNING id;";
                return await _executor.ExecuteScalarAsync<int>(connection, updateQuery, new { isRequired = newTechnologyDependency.IsRequired, id = existingDependency.Id });
            }

            return existingDependency.Id;
        }

        if (newTechnologyDependency.GetNotNullKeys().Count > 1)
        {
            throw new InvalidDataException($"technology dependency should link to only one at a time of [unit, skill, equipment], and currently has: {newTechnologyDependency.ToString()}");
        }

        try
        {
            // INSERT INTO technology_dependency (technology_id, is_required, unit_id, skill_id) VALUES(1, true, 1, null);
            var insertQuery = "INSERT INTO technology_dependency (technology_id, is_required, unit_id, skill_id, equipment_id) VALUES(@technologyId, @isRequired, @unitId, @skillId, @equipmentId) RETURNING id;";
            return await _executor.ExecuteScalarAsync<int>(connection, insertQuery, new { technologyId = newTechnologyDependency.TechnologyId, isRequired = newTechnologyDependency.IsRequired, unitId = newTechnologyDependency.UnitId, skillId = newTechnologyDependency.SkillId, equipmentId = newTechnologyDependency.EquipmentId });
        }
        catch (PostgresException ex)
        {
            Console.WriteLine($"Error: {ex.MessageText}. Detail: {ex.Detail}.");
            Console.WriteLine("Error: Returning -1");
            return -1;
        }
    }
}
