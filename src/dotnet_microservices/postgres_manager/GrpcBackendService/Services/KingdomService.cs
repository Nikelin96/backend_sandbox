
using DataAccessLibrary;
using DataAccessLibrary.Models;
using DataAccessLibrary.Repositories;
using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using GrpcService.KingdomRpc;

namespace GrpcBackendService.Services;
public sealed class KingdomService : KingdomRpc.KingdomRpcBase
{
    private readonly ILogger<KingdomService> _logger;
    private readonly KingdomTechnologyRepository _kigdomTechnologyRepository;
    private readonly KingdomRepository _kingdomRepository;
    private readonly KingdomUnitRepository _kingdomUnitRepository;

    private readonly KingdomTransactionRepository _kigdomTransactionRepository;

    public KingdomService(
        ILogger<KingdomService> logger,
        KingdomTechnologyRepository kigdomTechnologyRepository,
        KingdomRepository kingdomRepository,
        KingdomTransactionRepository kigdomTransactionRepository,
        KingdomUnitRepository kingdomUnitRepository)
    {
        _logger = logger;
        _kigdomTechnologyRepository = kigdomTechnologyRepository;
        _kingdomRepository = kingdomRepository;
        _kigdomTransactionRepository = kigdomTransactionRepository;
        _kingdomUnitRepository = kingdomUnitRepository;
    }

    public override async Task<KingdomResponse> GetKingdom(KingdomRequest request, ServerCallContext context)
    {
        //var results = await _kigdomTransactionRepository.RetrieveEntities(1);
        //var transaction = new KingdomTransaction{ KingdomId = 1, Type = TransactionType.Income, Food = 13, Wood = 13, Gold = 13, Stone = 13 };

        //var s = await _kingdomTransactionCreateRepository.Create(transaction);




        //var technology= new KingdomTechnology{KingdomId = 1, KingdomTransactionId =1, Name = "asdasd", ResearchStartTime =DateTime.Now,ResearchStatus = ResearchStatusType.InProgress, TechnologyDescription ="", TechnologyId = 1, TechnologyName = "sa"};
        //var s =await _kigdomTechnologyRepository.Create(technology);

        var results = await _kingdomUnitRepository.RetrieveEntities(1);

        var unit = new KingdomUnit{UnitId = 12};
        var s = await _kingdomUnitRepository.Create(unit);


        var kingdoms = await _kingdomRepository.RetrieveEntities();

        var kingdomResponse = new KingdomResponse
        {
            Name = kingdoms.First().Name
        };

        return await Task.FromResult(kingdomResponse);
    }

    public override async Task GetAllKingdoms(KingdomRequest request, IServerStreamWriter<KingdomResponse> responseStream, ServerCallContext context)
    {
        var kingdoms = await _kingdomRepository.RetrieveEntities();

        foreach (var kingdom in kingdoms)
        {
            await Task.Delay(1000);

            var kingdomResponse =  new KingdomResponse
            {
                Name = kingdom.Name
            };

            await responseStream.WriteAsync(kingdomResponse);
        }
    }

    public override async Task GetKingdomTechologies(KingdomTechnologyRequest request, IServerStreamWriter<KingdomTechnologyResponse> responseStream, ServerCallContext context)
    {
        //var config = new MapperConfiguration(cfg =>
        //{
        //    cfg.CreateMap<KingdomTechnology, KingdomTechnologyResponse>().ForMember(x => x.ResearchStartTime, opt => opt.MapFrom(src=> src.ResearchStartTime.ToUniversalTime().ToTimestamp()));
        //});

        //var mapper = config.CreateMapper();
        // or


        var kingdomTechnologies = await _kigdomTechnologyRepository.RetrieveEntities(request.KingdomTechnology);

        foreach (var technology in kingdomTechnologies)
        {
            await Task.Delay(1000);

            var kingdomResponse =  new KingdomTechnologyResponse
            {
                Name = technology.Name,
                TechnologyName = technology.TechnologyName,
                TechnologyDescription = technology.TechnologyDescription,
                ResearchStartTime = technology.ResearchStartTime.ToUniversalTime().ToTimestamp(),
                ResearchStatus = technology.ResearchStatus.ToString()
            };

            //var kingdomResponse2 = mapper.Map<KingdomTechnologyResponse>(technology);

            await responseStream.WriteAsync(kingdomResponse);
        }
    }
}
