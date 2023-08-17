using GrpcBackendService.DataAccess;
using GrpcBackendService.Models;

namespace GrpcBackendService.UnitsOfWork;
public sealed class CreateUnitJourney
{
    private readonly ICreateEntityCommand<Stat> _statCreationRepository;
    private readonly ICreateEntityCommand<Price> _priceCreationRepository;
    private readonly ICreateEntityCommand<Unit> _unitCreationRepository;

    public CreateUnitJourney(ICreateEntityCommand<Stat> statCreationRepository, ICreateEntityCommand<Price> priceCreationRepository, ICreateEntityCommand<Unit> unitCreationRepository)
    {
        _statCreationRepository = statCreationRepository;
        _priceCreationRepository = priceCreationRepository;
        _unitCreationRepository = unitCreationRepository;
    }

    public async Task CreateEquipment(Stat stat, Unit unit, Price price)
    {
        try
        {
            var statId = await _statCreationRepository.Create(stat);
            unit.StatId = statId;
            var equipmentId = await _unitCreationRepository.Create(unit);
            price.EquipmentId = equipmentId;
            var priceId  = await _priceCreationRepository.Create(price);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
            throw;
        }
    }
}
