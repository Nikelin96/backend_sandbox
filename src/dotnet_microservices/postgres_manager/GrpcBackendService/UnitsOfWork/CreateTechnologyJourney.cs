using GrpcBackendService.DataAccess;
using GrpcBackendService.Models;

namespace GrpcBackendService.UnitsOfWork;
public sealed class CreateTechnologyJourney
{
    private readonly ICreateEntityCommand<Price> _priceCreationRepository;
    private readonly ICreateEntityCommand<Technology> _technologyCreationRepository;

    public CreateTechnologyJourney(ICreateEntityCommand<Price> priceCreationRepository, ICreateEntityCommand<Technology> technologyCreationRepository)
    {
        _priceCreationRepository = priceCreationRepository;
        _technologyCreationRepository = technologyCreationRepository;
    }

    public async Task CreateTechnology(Technology technology, Price price)
    {
        try
        {
            var technologyId = await _technologyCreationRepository.Create(technology);
            price.TechnologyId = technologyId;
            var priceId = await _priceCreationRepository.Create(price);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
            throw;
        }
    }
}