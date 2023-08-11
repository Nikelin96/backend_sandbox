using Grpc.Core;
using GrpcBackendService.Models;

namespace GrpcBackendService.Services;

public sealed class GreeterService : Greeter.GreeterBase
{
    private readonly ILogger<GreeterService> _logger;
    private readonly IDataRepository<Kingdom> _repository;
    public GreeterService(ILogger<GreeterService> logger, IDataRepository<Kingdom> dataRepository)
    {
        _logger = logger;
        _repository = dataRepository;
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
        var kingdom = await _repository.GetById(1);

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
