namespace GrpcBackendService.Services
{
    using Dapper;
    using GrpcBackendService.Helpers;
    using GrpcBackendService.Models;
    using System.Diagnostics;

    public class KingdomRepository : IDataRepository<Kingdom>
    {
        private DataContext _context;

        public KingdomRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Kingdom>> GetAll()
        {

            //var sql = """
            //    SELECT * FROM kingdom AS kn 
            //    JOIN kingdom_technology AS kt
            //    ON kt.kingdom_id = kn.id
            //""";
            using var connection = _context.CreateConnection();

            var sql = @"
                SELECT * FROM kingdom;
            ";

            return await connection.QueryAsync<Kingdom>(sql);

            //return await connection.QueryAsync<Kingdom, Technology, Kingdom>(sql, (kingdom, technology) =>
            //{
            //    kingdom.Technology = technology;
            //    return kingdom;
            //});
        }

        public async Task<Kingdom> GetById(int id)
        {
            var query = @"
             SELECT * FROM kingdom WHERE id = @kingdomId;
             SELECT * FROM kingdom_technology WHERE kingdom_id = @kingdomId;
            ";

            using var connection = _context.CreateConnection();

            try
            {
                var results = await connection.QueryMultipleAsync(query, new { @kingdomId = id });

                var kingdom = results.ReadSingle<Kingdom>();
                var kingdomTechnologies = results.Read<Technology>();

                kingdom.Technologies.AddRange(kingdomTechnologies);

                return kingdom;

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }

            return null;
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
