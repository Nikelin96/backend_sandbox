using Dapper;
using System.Data;

namespace GrpcBackendService.DataAccess;
public interface IRetrieveEntitesByIdQuery<T>
{
    public Task<IEnumerable<T>> RetrieveEntities(int id);
}

public interface IRetrieveEntitesQuery<T>
{
    public Task<IEnumerable<T>> RetrieveEntities();
}

public interface ICreateEntityCommand<T>
{
    public Task<int> Create(T entity);
}

public interface ISetTechnologyDependency<T>
{
    public Task<int> SetTechnologyDependency(T entity);
}

public interface IDataAccessExecutor
{
    public Task<T> ExecuteScalarAsync<T>(IDbConnection connection, string sql, object entity);
    public Task<IEnumerable<T>> QueryAsync<T>(IDbConnection connection, string sql, object? entity = null);
}

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

public interface IConnectionCreator
{
    IDbConnection Create();
}