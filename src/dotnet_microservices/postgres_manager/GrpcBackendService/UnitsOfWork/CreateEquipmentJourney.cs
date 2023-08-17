using DataAccessLibrary.Models;
using DataAccessLibrary.Repositories;

namespace GrpcBackendService.UnitsOfWork;
public sealed class CreateEquipmentJourney
{
    private readonly StatRepository _statRepository;
    private readonly PriceRepository _priceRepository;
    private readonly EquipmentRepository _equipmentRepository;

    public CreateEquipmentJourney(StatRepository statRepository, PriceRepository priceRepository, EquipmentRepository equipmentRepository)
    {
        _statRepository = statRepository;
        _priceRepository = priceRepository;
        _equipmentRepository = equipmentRepository;
    }

    public async Task CreateEquipment(Stat stat, Equipment equipment, Price price)
    {
        try
        {
            var statId = await _statRepository.Create(stat);
            equipment.StatId = statId;
            var equipmentId = await _equipmentRepository.Create(equipment);
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
