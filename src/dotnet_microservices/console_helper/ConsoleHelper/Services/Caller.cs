﻿using Grpc.Core;
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
                 new HelloRequest { Name = "Nik" });
        }

        public async void GetMany(string address)
        {
            using var channel = GrpcChannel.ForAddress(address);

            var client = new KingdomRpc.KingdomRpcClient(channel);

            try
            {
                var cancellationToken = new CancellationTokenSource(TimeSpan.FromSeconds(10));
                using var streamingCall = client.GetKingdomWithTechologies(new HelloRequest(), cancellationToken: cancellationToken.Token);
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
