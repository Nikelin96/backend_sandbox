using GrpcBackendService.Models;

namespace GrpcBackendService.Services;

public sealed class TechnologyRepository : IDataRepository<Technology>
{
    public Task Create(Technology user)
    {
        throw new NotImplementedException();
    }

    public Task Delete(int id)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<Technology>> GetAll()
    {
        throw new NotImplementedException();
    }

    public Task<Technology> GetById(int id)
    {
        throw new NotImplementedException();
    }

    public Task Update(Technology user)
    {
        throw new NotImplementedException();
    }
}
