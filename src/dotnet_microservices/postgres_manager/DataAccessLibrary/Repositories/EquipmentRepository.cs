using DataAccessLibrary.Models;

namespace DataAccessLibrary.Repositories
{
    public sealed class EquipmentRepository : ICreateEntityCommand<Equipment>
    {
        private readonly IConnectionCreator _context;
        private readonly IDataAccessExecutor _executor;

        public EquipmentRepository(IConnectionCreator connectionCreator, IDataAccessExecutor repository)
        {
            _context = connectionCreator;
            _executor = repository;
        }

        public async Task<int> Create(Equipment equipment)
        {
            // INSERT INTO equipment (name, stat_id) VALUES ('chain mail', 3); 
            var sql = @"INSERT INTO equipment (name, stat_id) VALUES (@Name, @StatId) RETURNING id;";

            using var connection = _context.Create();

            return await _executor.ExecuteScalarAsync<int>(connection, sql, equipment);
        }
    }
}