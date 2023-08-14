using Grpc.Core;
using GrpcBackendService.DataAccess;
using GrpcBackendService.Models;
using GrpcBackendService.UnitsOfWork;

namespace GrpcBackendService.Services;

public sealed class KingdomService : KingdomRpc.KingdomRpcBase
{
    private readonly ILogger<KingdomService> _logger;
    private readonly IRetrieveEntitesByIdQuery<KingdomTechnology> _kigdomTechnologyRepository;
    private readonly ICreateEntityCommand<Kingdom> _kingdomCreateRepository;
    private readonly IRetrieveEntitesQuery<Kingdom> _kingdomGetRepository;
    private readonly CreateTechnologyJourney _createTechnologyStory;
    private readonly CreateEquipmentJourney _createEquipmentJourney;
    private readonly CreateSkillJourney _createSkilJourney;

    public KingdomService(ILogger<KingdomService> logger, IRetrieveEntitesByIdQuery<KingdomTechnology> kigdomTechnologyRepository, ICreateEntityCommand<Kingdom> kingdomCreateRepository, IRetrieveEntitesQuery<Kingdom> kingdomGetRepository, CreateTechnologyJourney createTechnologyStory, CreateEquipmentJourney createEquipmentJourney, CreateSkillJourney createSkilJourney)
    {
        _logger = logger;
        _kigdomTechnologyRepository = kigdomTechnologyRepository;
        _kingdomCreateRepository = kingdomCreateRepository;
        _kingdomGetRepository = kingdomGetRepository;
        _createTechnologyStory = createTechnologyStory;
        _createEquipmentJourney = createEquipmentJourney;
        _createSkilJourney = createSkilJourney;
    }

    public override async Task<HelloReply> GetAllKingdoms(HelloRequest request, ServerCallContext context)
    {
        // var kingdoms = await _kingdomGetRepository.RetrieveEntities();
        var kingdomTechnologies = await _kigdomTechnologyRepository.RetrieveEntities(1);
        var kingdom = new Kingdom{ Name = "Italy", Rank = 1, ContinentId = 1};
        await _kingdomCreateRepository.Create(kingdom);


        // create technology journey
        var technology = new Technology{Name = "Sword", Description = "Simple bronze sword for beginners", ResearchTime = 100 };
        var technologyPrice = new Price{Wood = 5, Food =5, Gold = 5, Stone = 5 };
        await _createTechnologyStory.CreateTechnology(technology, technologyPrice);

        // create equipment journey
        var stat = new Stat { HealthPoints = 100 };
        var equipment = new Equipment { Name = "Pike" };
        var equipmentPrice = new Price { Gold = 10 };
        await _createEquipmentJourney.CreateEquipment(stat, equipment, equipmentPrice);

        // create skill journey
        var skillStat1 = new Stat { HealthPoints = 5 };
        var skill1 = new Skill { Type = SkillType.Heal };
        await _createSkilJourney.CreateSkill(skillStat1, skill1);

        // create skill journey
        var skillStat2 = new Stat { DefensePoints = 5 };
        var skill2 = new Skill { Type = SkillType.Defend };
        await _createSkilJourney.CreateSkill(skillStat2, skill2);

        // create skill journey
        var skillStat3 = new Stat { DamagePoints = 7 };
        var skill3 = new Skill { Type = SkillType.Rally };
        await _createSkilJourney.CreateSkill(skillStat3, skill3);

        var reply = new HelloReply
        {
            Message = "hello " + request.Name
        };
        return await Task.FromResult(new HelloReply
        {
            Message = "Hello " + request.Name
        });
    }

    public override async Task GetKingdomWithTechologies(HelloRequest request, IServerStreamWriter<HelloReply> responseStream, ServerCallContext context)
    {
        var reply = new HelloReply
        {
            Message = "hello " + request.Name
        };

        var i = 0;
        while (!context.CancellationToken.IsCancellationRequested && i < 10)
        {
            await Task.Delay(1000);
            await responseStream.WriteAsync(reply);
            i++;
        }
    }
}
