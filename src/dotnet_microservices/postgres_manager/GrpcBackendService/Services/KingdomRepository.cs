namespace GrpcBackendService.Services
{
    using Dapper;
    using GrpcBackendService.Helpers;
    using GrpcBackendService.Models;

    public class KingdomRepository : IDataRepository<Kingdom>
    {
        private DataContext _context;

        public KingdomRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Kingdom>> GetAll()
        {
            using var connection = _context.CreateConnection();

            var sql = """
                SELECT * FROM kingdom AS kn 
                JOIN kingdom_technology AS kt
                ON kt.kingdom_id = kn.id
            """;

            return await connection.QueryAsync<Kingdom, Technology, Kingdom>(sql, (kingdom, technology) => { kingdom.Technologies.Add(technology); return kingdom; });
        }

        public async Task<Kingdom> GetById(int id)
        {
            return await Task.FromResult<Kingdom>(null);
            //    using var connection = _context.CreateConnection();
            //    var sql = """
            //    SELECT * FROM Users 
            //    WHERE Id = @id
            //""";
            //    return await connection.QuerySingleOrDefaultAsync<Kingdom>(sql, new { id });
        }

        public async Task Create(Kingdom user)
        {
            //    using var connection = _context.CreateConnection();
            //    var sql = """
            //    INSERT INTO Users (Title, FirstName, LastName, Email, Role, PasswordHash)
            //    VALUES (@Title, @FirstName, @LastName, @Email, @Role, @PasswordHash)
            //""";
            //    await connection.ExecuteAsync(sql, user);
        }

        public async Task Update(Kingdom user)
        {
            //    using var connection = _context.CreateConnection();
            //    var sql = """
            //    UPDATE Users 
            //    SET Title = @Title,
            //        FirstName = @FirstName,
            //        LastName = @LastName, 
            //        Email = @Email, 
            //        Role = @Role, 
            //        PasswordHash = @PasswordHash
            //    WHERE Id = @Id
            //""";
            //    await connection.ExecuteAsync(sql, user);
        }

        public async Task Delete(int id)
        {
            //    using var connection = _context.CreateConnection();
            //    var sql = """
            //    DELETE FROM Users 
            //    WHERE Id = @id
            //""";
            //    await connection.ExecuteAsync(sql, new { id });
        }
    }
}
