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

    public async Task<SqlMapper.GridReader> QueryMultiple(IDbConnection connection, string sql, object? entity = null)
    {
        return await connection.QueryMultipleAsync(sql, entity);
    }

    public async Task<T> QuerySingleAsync<T>(IDbConnection connection, string sql, object? entity = null)
    {
        return await connection.QuerySingleAsync<T>(sql, entity);
    }
}