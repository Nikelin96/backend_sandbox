using Grpc.Core;
using GrpcBackendService.DataAccess;
using GrpcBackendService.Models;

namespace GrpcBackendService.Services;

public sealed class GreeterService : Greeter.GreeterBase
{
    private readonly ILogger<GreeterService> _logger;
    private readonly IDataRepository<KingdomTechnology> _repository;
    private readonly IDataRepository<Kingdom> _kingdomRepository;
    public GreeterService(ILogger<GreeterService> logger, IDataRepository<KingdomTechnology> dataRepository, IDataRepository<Kingdom> kingdomRepository)
    {
        _logger = logger;
        _repository = dataRepository;
        _kingdomRepository = kingdomRepository;
    }

    public override async Task<HelloReply> GetAllKingdoms(HelloRequest request, ServerCallContext context)
    {
        var kingdoms = await _repository.GetAll();

        return await Task.FromResult(new HelloReply
        {
            Message = "Hello " + request.Name
        });
    }

    public override async Task GetKingdomWithTechologies(HelloRequest request, IServerStreamWriter<HelloReply> responseStream, ServerCallContext context)
    {
        //var kingdom = await _repository.GetById(1);

        var kingdom = new Kingdom{ Name = "Italy", Rank = 1, ContinentId = 1};

        await _kingdomRepository.Create(kingdom);

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
