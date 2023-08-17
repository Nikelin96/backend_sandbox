
using DataAccessLibrary;
using DataAccessLibrary.Models;

namespace GrpcBackendService.UnitsOfWork;
public sealed class CreateEquipmentJourney
{
    private readonly ICreateEntityCommand<Stat> _statCreationRepository;
    private readonly ICreateEntityCommand<Price> _priceCreationRepository;
    private readonly ICreateEntityCommand<Equipment> _equipmentCreationRepository;

    public CreateEquipmentJourney(ICreateEntityCommand<Stat> statCreationRepository, ICreateEntityCommand<Price> priceCreationRepository, ICreateEntityCommand<Equipment> equipmentCreationRepository)
    {
        _statCreationRepository = statCreationRepository;
        _priceCreationRepository = priceCreationRepository;
        _equipmentCreationRepository = equipmentCreationRepository;
    }

    public async Task CreateEquipment(Stat stat, Equipment equipment, Price price)
    {
        try
        {
            var statId = await _statCreationRepository.Create(stat);
            equipment.StatId = statId;
            var equipmentId = await _equipmentCreationRepository.Create(equipment);
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
