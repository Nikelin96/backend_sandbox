using Grpc.Core;
using GrpcBackendService;
using GrpcBackendService.Models;

namespace GrpcBackendService.Services;

public class GreeterService : Greeter.GreeterBase
{
    private readonly ILogger<GreeterService> _logger;
    private readonly IDataRepository<Kingdom> _repository;
    public GreeterService(ILogger<GreeterService> logger, IDataRepository<Kingdom> dataRepository)
    {
        _logger = logger;
        _repository = dataRepository;
    }

    public override async Task<HelloReply> SayHello(HelloRequest request, ServerCallContext context)
    {
        var x = await _repository.GetAll();

        return await Task.FromResult(new HelloReply
        {
            Message = "Hello " + request.Name
        });
    }
}
