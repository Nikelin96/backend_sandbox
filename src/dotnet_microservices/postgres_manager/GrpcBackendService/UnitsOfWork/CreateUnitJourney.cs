using DataAccessLibrary;
using DataAccessLibrary.Models;
using DataAccessLibrary.Repositories;

namespace GrpcBackendService.UnitsOfWork;
public sealed class CreateUnitJourney
{
    private readonly StatRepository _statRepository;
    private readonly PriceRepository _priceRepository;
    private readonly UnitRepository _unitRepository;

    public CreateUnitJourney(StatRepository statRepository, PriceRepository priceRepository, UnitRepository unitRepository)
    {
        _statRepository = statRepository;
        _priceRepository = priceRepository;
        _unitRepository = unitRepository;
    }

    public async Task CreateEquipment(Stat stat, Unit unit, Price price)
    {
        try
        {
            var statId = await _statRepository.Create(stat);
            unit.StatId = statId;
            var equipmentId = await _unitRepository.Create(unit);
            price.EquipmentId = equipmentId;
            var priceId  = await _priceRepository.Create(price);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
            throw;
        }
    }
}
