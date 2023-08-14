namespace GrpcBackendService.UnitsOfWork;

public abstract class BaseJourney
{
    public async Task ExecuteJourney()
    {
        try
        {
            await CreateEntities();
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
            throw;
        }
    }

    protected abstract Task CreateEntities();
}

