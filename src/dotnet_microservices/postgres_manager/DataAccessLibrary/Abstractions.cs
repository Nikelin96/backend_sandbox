using Dapper;
using System.Data;

namespace DataAccessLibrary;

public interface IConnectionCreator
{
    IDbConnection Create();
}


public interface IDataAccessExecutor
{
    public Task<T> ExecuteScalarAsync<T>(IDbConnection connection, string sql, object entity);
    public Task<IEnumerable<T>> QueryAsync<T>(IDbConnection connection, string sql, object? entity = null);
    public Task<SqlMapper.GridReader> QueryMultiple(IDbConnection connection, string sql, object? entity = null);
    public Task<T> QuerySingleAsync<T>(IDbConnection connection, string sql, object? entity = null);
}

