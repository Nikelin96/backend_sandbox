namespace GrpcBackendService.Services;
public interface IDataRepository<T> where T : class, new()
{
    public Task<IEnumerable<T>> GetAll();
    public Task<T> GetById(int id);
    public Task Create(T user);
    public Task Update(T user);
    public Task Delete(int id);
}