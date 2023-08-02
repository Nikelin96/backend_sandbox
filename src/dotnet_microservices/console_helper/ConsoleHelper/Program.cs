// See https://aka.ms/new-console-template for more information

using ConsoleHelper;
using Grpc.Net.Client;
using Microsoft.Extensions.Configuration;


var configuraion = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", optional: false).Build();

var address = configuraion.GetConnectionString("TargetServer");
using var channel = GrpcChannel.ForAddress(address);
var client = new Greeter.GreeterClient(channel);
var reply = await client.SayHelloAsync(
                 new HelloRequest { Name = "Nik" });
Console.WriteLine("Greeting: " + reply.Message);
Console.WriteLine("Press any key to exit...");
Console.ReadKey();