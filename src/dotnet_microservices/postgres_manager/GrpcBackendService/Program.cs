using GrpcBackendService.DataAccess;
using GrpcBackendService.DataAccess.Repositories;
using GrpcBackendService.Models;
using GrpcBackendService.Services;
using GrpcBackendService.UnitsOfWork;

var builder = WebApplication.CreateBuilder(args);

// Additional configuration is required to successfully run gRPC on macOS.
// For instructions on how to configure Kestrel and gRPC clients on macOS, visit https://go.microsoft.com/fwlink/?linkid=2099682

// Add services to the container.
{
    var services = builder.Services;

    services.AddSingleton<DataContext>();
    services.AddSingleton<IConnectionCreator, DataContext>();
    services.AddSingleton<IDataAccessExecutor, DapperExecutor>();
    services.AddScoped<IRetrieveEntitesByIdQuery<KingdomTechnology>, KingdomTechnologyRepository>();
    services.AddScoped<IRetrieveEntitesQuery<Kingdom>, KingdomRepository>();
    services.AddScoped<ICreateEntityCommand<Kingdom>, KingdomRepository>();
    services.AddScoped<ICreateEntityCommand<Technology>, TechnologyRepository>();
    services.AddScoped<ICreateEntityCommand<Price>, PriceRepository>();
    services.AddScoped<ICreateEntityCommand<Equipment>, EquipmentRepository>();
    services.AddScoped<ICreateEntityCommand<Stat>, StatRepository>();
    services.AddScoped<ICreateEntityCommand<Skill>, SkillRepository>();
    services.AddScoped<ISetTechnologyDependency<TechnologyDependency>, TechnologyRepository>();
    services.AddScoped<CreateTechnologyJourney, CreateTechnologyJourney>();
    services.AddScoped<CreateEquipmentJourney, CreateEquipmentJourney>();
    services.AddScoped<CreateSkillJourney, CreateSkillJourney>();

    services.AddGrpc();
}

var app = builder.Build();

// ensure database and tables exist
{
    using var scope = app.Services.CreateScope();
    var context = scope.ServiceProvider.GetRequiredService<DataContext>();
    context.Init();
}

// Configure the HTTP request pipeline.
{
    app.MapGrpcService<KingdomService>();
    app.MapGrpcService<GameService>();
    app.MapGet("/king", () => "Communication with gRPC endpoints must be made through a gRPC client. To learn how to create a client, visit: https://go.microsoft.com/fwlink/?linkid=2086909");
    app.Run();
}