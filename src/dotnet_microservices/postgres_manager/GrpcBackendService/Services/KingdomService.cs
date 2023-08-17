
using DataAccessLibrary;
using DataAccessLibrary.Models;
using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using GrpcService.KingdomRpc;

namespace GrpcBackendService.Services;
public sealed class KingdomService : KingdomRpc.KingdomRpcBase
{
    private readonly ILogger<KingdomService> _logger;
    private readonly IRetrieveEntitesByIdQuery<KingdomTechnology> _kigdomTechnologyRepository;
    private readonly ICreateEntityCommand<KingdomTechnology> _test;
    private readonly IRetrieveEntitesQuery<Kingdom> _kingdomGetRepository;

    private readonly IRetrieveEntitesByIdQuery<KingdomTransaction> _kigdomTransactionRepository;
    private readonly ICreateEntityCommand<KingdomTransaction> _kingdomTransactionCreateRepository;

    public KingdomService(ILogger<KingdomService> logger,
        IRetrieveEntitesByIdQuery<KingdomTechnology> kigdomTechnologyRepository,
        IRetrieveEntitesQuery<Kingdom> kingdomGetRepository,
        IRetrieveEntitesByIdQuery<KingdomTransaction> kigdomTransactionRepository,
        ICreateEntityCommand<KingdomTransaction> kingdomTransactionGetRepository,
        ICreateEntityCommand<KingdomTechnology> test)
    {
        _logger = logger;
        _kigdomTechnologyRepository = kigdomTechnologyRepository;
        _kingdomGetRepository = kingdomGetRepository;
        _kigdomTransactionRepository = kigdomTransactionRepository;
        _kingdomTransactionCreateRepository = kingdomTransactionGetRepository;
        _test = test;
    }

    public override async Task<KingdomResponse> GetKingdom(KingdomRequest request, ServerCallContext context)
    {
        //var results = await _kigdomTransactionRepository.RetrieveEntities(1);
        //var transaction = new KingdomTransaction{ KingdomId = 1, Type = TransactionType.Income, Food = 13, Wood = 13, Gold = 13, Stone = 13 };

        //var s = await _kingdomTransactionCreateRepository.Create(transaction);





        var technology= new KingdomTechnology{KingdomId = 1, KingdomTransactionId =1, Name = "asdasd", ResearchStartTime =DateTime.Now,ResearchStatus = "in progress", TechnologyDescription ="", TechnologyId = 1, TechnologyName = "sa"};
        var s =await _test.Create(technology);



        var kingdoms = await _kingdomGetRepository.RetrieveEntities();

        var kingdomResponse = new KingdomResponse
        {
            Name = kingdoms.First().Name
        };

        return await Task.FromResult(kingdomResponse);
    }

    public override async Task GetAllKingdoms(KingdomRequest request, IServerStreamWriter<KingdomResponse> responseStream, ServerCallContext context)
    {
        var kingdoms = await _kingdomGetRepository.RetrieveEntities();

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
                ResearchStatus = technology.ResearchStatus
            };

            //var kingdomResponse2 = mapper.Map<KingdomTechnologyResponse>(technology);

            await responseStream.WriteAsync(kingdomResponse);
        }
    }
}
