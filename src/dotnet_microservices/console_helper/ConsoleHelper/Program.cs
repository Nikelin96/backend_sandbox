// See https://aka.ms/new-console-template for more information

using ConsoleHelper.Services;
using Grpc.Net.Client;
using Microsoft.Extensions.Configuration;


var configuraion = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", optional: false).Build();

var address = configuraion.GetConnectionString("TargetServer");
using var channel = GrpcChannel.ForAddress(address);


new Caller().CreateOne(address);

//Console.WriteLine("Greeting: " + reply.Message);B
Console.WriteLine("Press any key to exit...");
Console.ReadKey();