using Dapper;
using System.Data;
using DataAccessLibrary;

public class DapperExecutor : IDataAccessExecutor
{
    public async Task<T> ExecuteScalarAsync<T>(IDbConnection connection, string sql, object entity)
    {
        return await connection.ExecuteScalarAsync<T>(sql, entity);
    }

    public async Task<IEnumerable<T>> QueryAsync<T>(IDbConnection connection, string sql, object? entity = null)
    {
        return await connection.QueryAsync<T>(sql, entity);
    }
}