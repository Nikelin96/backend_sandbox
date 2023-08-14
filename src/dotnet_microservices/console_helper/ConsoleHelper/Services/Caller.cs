using ConsoleHelper.Game;
using ConsoleHelper.Kingdom;
using Grpc.Core;
using Grpc.Net.Client;

namespace ConsoleHelper.Services
{
    internal class Caller
    {

        public void GetOne(string address)
        {
            using var channel = GrpcChannel.ForAddress(address);

            var client = new KingdomRpc.KingdomRpcClient(channel);
            var reply =  client.GetAllKingdoms(
                 new ConsoleHelper.Kingdom.KingdomRequest { ContinentId = 1 });
        }

        public void CreateOne(string address)
        {
            using var channel = GrpcChannel.ForAddress(address);

            var client = new GameRpc.GameRpcClient(channel);
            var reply =  client.CreateEntities(
                 new ConsoleHelper.Game.KingdomRequest { ContinentId = 1 });
        }

        public async void GetMany(string address)
        {
            using var channel = GrpcChannel.ForAddress(address);

            var client = new KingdomRpc.KingdomRpcClient(channel);

            try
            {
                var cancellationToken = new CancellationTokenSource(TimeSpan.FromSeconds(10));
                using var streamingCall = client.GetKingdomTechologies(new KingdomTechnologyRequest(), cancellationToken: cancellationToken.Token);
                await foreach (var weatherData in streamingCall.ResponseStream.ReadAllAsync(cancellationToken: cancellationToken.Token))
                {
                    Console.WriteLine(weatherData);
                }
                Console.WriteLine("Stream completed.");
            }
            catch (RpcException ex) when (ex.StatusCode == StatusCode.Cancelled)
            {
                Console.WriteLine("Stream cancelled.");
            }

        }
    }
}
