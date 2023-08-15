namespace GrpcBackendService.Services;

using Grpc.Core;
using GrpcBackendService.DataAccess;
using GrpcBackendService.Models;
using GrpcBackendService.UnitsOfWork;
using GrpcService.GameRpc;

public sealed class GameService : GameRpc.GameRpcBase
{
    private readonly ILogger<KingdomService> _logger;
    private readonly CreateTechnologyJourney _createTechnologyStory;
    private readonly CreateEquipmentJourney _createEquipmentJourney;
    private readonly CreateSkillJourney _createSkilJourney;
    private readonly ICreateEntityCommand<Kingdom> _kingdomCreateRepository;
    private readonly ISetTechnologyDependency<TechnologyDependency> _technologyDependencyRepository;

    public GameService(ILogger<KingdomService> logger, ICreateEntityCommand<Kingdom> kingdomCreateRepository, CreateTechnologyJourney createTechnologyStory, CreateEquipmentJourney createEquipmentJourney, CreateSkillJourney createSkilJourney, ISetTechnologyDependency<TechnologyDependency> technologyDependencyRepository)
    {
        _logger = logger;
        _createTechnologyStory = createTechnologyStory;
        _createEquipmentJourney = createEquipmentJourney;
        _createSkilJourney = createSkilJourney;
        _kingdomCreateRepository = kingdomCreateRepository;
        _technologyDependencyRepository = technologyDependencyRepository;
    }

    public override async Task<KingdomResponse> CreateEntities(KingdomRequest request, ServerCallContext context)
    {
        //var kingdom = new Kingdom{ Name = "Italy", Rank = 1, ContinentId = 1};
        //await _kingdomCreateRepository.Create(kingdom);

        //// create technology journey
        //var technology = new Technology{Name = "Sword", Description = "Simple bronze sword for beginners", ResearchTime = 100 };
        //var technologyPrice = new Price{Wood = 5, Food = 5, Gold = 5, Stone = 5 };
        //var technologyId = await _createTechnologyStory.CreateTechnology(technology, technologyPrice);

        try
        {

            var technologyDependency = new TechnologyDependency { TechnologyId = 2343, UnitId = 1, SkillId = 1, EquipmentId = 1};
            await _technologyDependencyRepository.SetTechnologyDependency(technologyDependency);

            var technologyDependency2 = new TechnologyDependency { TechnologyId = 1, IsRequired = false,  UnitId = 1};
            await _technologyDependencyRepository.SetTechnologyDependency(technologyDependency2);

            var technologyDependency3 = new TechnologyDependency { TechnologyId = 1, UnitId = 1, SkillId = 1, EquipmentId = 2 };
            await _technologyDependencyRepository.SetTechnologyDependency(technologyDependency3);

            var technologyDependency4 = new TechnologyDependency { TechnologyId = 3, IsRequired = false, SkillId = 234 };
            await _technologyDependencyRepository.SetTechnologyDependency(technologyDependency4);

        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }

        //// create equipment journey
        //var stat = new Stat { HealthPoints = 100 };
        //var equipment = new Equipment { Name = "Pike" };
        //var equipmentPrice = new Price { Gold = 10 };
        //await _createEquipmentJourney.CreateEquipment(stat, equipment, equipmentPrice);

        //// create skill journey
        //var skillStat1 = new Stat { HealthPoints = 5 };
        //var skill1 = new Skill { Type = SkillType.Heal };
        //await _createSkilJourney.CreateSkill(skillStat1, skill1);

        //// create skill journey
        //var skillStat2 = new Stat { DefensePoints = 5 };
        //var skill2 = new Skill { Type = SkillType.Defend };
        //await _createSkilJourney.CreateSkill(skillStat2, skill2);

        //// create skill journey
        //var skillStat3 = new Stat { DamagePoints = 7 };
        //var skill3 = new Skill { Type = SkillType.Rally };
        //await _createSkilJourney.CreateSkill(skillStat3, skill3);

        var reply = new KingdomResponse();
        return await Task.FromResult(reply);
    }
}
