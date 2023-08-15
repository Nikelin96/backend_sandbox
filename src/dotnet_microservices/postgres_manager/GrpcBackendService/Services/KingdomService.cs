namespace GrpcBackendService.Services;

using AutoMapper;
using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using GrpcBackendService.DataAccess;
using GrpcBackendService.Models;
using GrpcService.KingdomRpc;

public sealed class KingdomService : KingdomRpc.KingdomRpcBase
{
    private readonly ILogger<KingdomService> _logger;
    private readonly IRetrieveEntitesByIdQuery<KingdomTechnology> _kigdomTechnologyRepository;
    private readonly IRetrieveEntitesQuery<Kingdom> _kingdomGetRepository;

    public KingdomService(ILogger<KingdomService> logger, IRetrieveEntitesByIdQuery<KingdomTechnology> kigdomTechnologyRepository, IRetrieveEntitesQuery<Kingdom> kingdomGetRepository)
    {
        _logger = logger;
        _kigdomTechnologyRepository = kigdomTechnologyRepository;
        _kingdomGetRepository = kingdomGetRepository;
    }

    public override async Task<KingdomResponse> GetKingdom(KingdomRequest request, ServerCallContext context)
    {
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
