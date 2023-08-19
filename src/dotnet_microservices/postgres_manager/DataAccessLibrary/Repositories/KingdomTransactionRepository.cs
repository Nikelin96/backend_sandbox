using DataAccessLibrary.Models;

namespace DataAccessLibrary.Repositories;
public sealed class KingdomTransactionRepository
{
    private readonly IConnectionCreator _connectionCreator;
    private readonly IDataAccessExecutor _executor;

    public KingdomTransactionRepository(IConnectionCreator connectionCreator, IDataAccessExecutor executor)
    {
        _connectionCreator = connectionCreator;
        _executor = executor;
    }

    public async Task<IEnumerable<KingdomTransaction>> RetrieveEntities(int id)
    {
        using var connection = _connectionCreator.Create();

        var sql = @"SELECT * FROM kingdom_transaction WHERE kingdom_id = @KingdomId;";

        return await _executor.QueryAsync<KingdomTransaction>(connection, sql, new { KingdomId = id });
    }

    public async Task<int> Create(KingdomTransaction kingdom)
    {
        // INSERT INTO kingdom_transaction (kingdom_id, type, wood, food, gold, stone) VALUES (1, 'income', 200, 200, 200, 200);
        var sql = @"INSERT INTO kingdom_transaction(kingdom_id, type, wood, food, gold, stone) VALUES (@KingdomId, @Type::transaction_type, @Wood, @Food, @Gold, @Stone) RETURNING id;";

        using var connection = _connectionCreator.Create();

        return await _executor.ExecuteScalarAsync<int>(connection, sql, new { KingdomId = kingdom.KingdomId, Type = kingdom.Type.ToPostgreEnum(), Wood = kingdom.Wood, Food = kingdom.Food, Gold = kingdom.Gold, Stone = kingdom.Stone });
    }

    public async Task<IEnumerable<KingdomTransaction>> GetKingdomTransactionsForKingdomTechnology(KingdomTechnology kingdomTecnology)
    {
        // check that kingdom_transaction exists, and its value equals to technology price
        var queryKingdomTransaction =  @"SELECT * FROM kingdom_transaction WHERE kingdom_id = @KingdomId AND technology_id = @TechnologyId";

        if (kingdomTecnology.UnitId.HasValue)
        {
            queryKingdomTransaction += " AND unit_id = @UnitId";
        }
        else
        {
            queryKingdomTransaction += " AND unit_id IS NULL";
        }
        if (kingdomTecnology.SkillId.HasValue)
        {
            queryKingdomTransaction += " AND skill_id = @SkillId";
        }
        else
        {
            queryKingdomTransaction += " AND skill_id IS NULL";
        }
        if (kingdomTecnology.EquipmentId.HasValue)
        {
            queryKingdomTransaction += " AND equipment_id = @EquipmentId";
        }
        else
        {
            queryKingdomTransaction += " AND equipment_id IS NULL";
        }

        queryKingdomTransaction += ";";

        var connection = _connectionCreator.Create();
        return await _executor.QueryAsync<KingdomTransaction>(connection, queryKingdomTransaction, kingdomTecnology);
    }
}