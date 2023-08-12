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
    public Task Create(T entity);
}
