using DataAccessLibrary.Models;
using DataAccessLibrary.Repositories;

namespace GrpcBackendService.UnitsOfWork;
public sealed class CreateTechnologyJourney
{

    private readonly PriceRepository _priceRepository;
    private readonly TechnologyRepository _technologyRepository;

    public CreateTechnologyJourney(PriceRepository priceRepository, TechnologyRepository technologyRepository)
    {
        _priceRepository = priceRepository;
        _technologyRepository = technologyRepository;
    }

    public async Task<int> CreateTechnology(Technology technology, Price price)
    {
        try
        {
            var technologyId = await _technologyRepository.Create(technology);
            price.TechnologyId = technologyId;
            var priceId = await _priceRepository.Create(price);

            return priceId;
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
            throw;
        }
    }
}