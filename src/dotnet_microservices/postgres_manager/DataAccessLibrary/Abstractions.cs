
using System.Data;

namespace DataAccessLibrary;
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

public interface IConnectionCreator
{
    IDbConnection Create();
}