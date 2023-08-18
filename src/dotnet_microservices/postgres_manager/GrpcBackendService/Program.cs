using DataAccessLibrary;
using DataAccessLibrary.Repositories;
using GrpcBackendService.DataAccess.Repositories;
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

    services.AddScoped<TechnologyRepository, TechnologyRepository>();
    services.AddScoped<PriceRepository, PriceRepository>();

    services.AddScoped<EquipmentRepository, EquipmentRepository>();
    services.AddScoped<StatRepository, StatRepository>();
    services.AddScoped<SkillRepository, SkillRepository>();

    services.AddScoped<UnitRepository, UnitRepository>();
    services.AddScoped<UnitSkillRepository, UnitSkillRepository>();
    services.AddScoped<UnitEquipmentRepository, UnitEquipmentRepository>();

    services.AddScoped<KingdomTransactionRepository, KingdomTransactionRepository>();
    services.AddScoped<KingdomTechnologyRepository, KingdomTechnologyRepository>();
    services.AddScoped<KingdomUnitRepository, KingdomUnitRepository>();
    services.AddScoped<KingdomRepository, KingdomRepository>();

    services.AddScoped<CreateTechnologyJourney, CreateTechnologyJourney>();
    services.AddScoped<CreateKingdomTechnologyJourney, CreateKingdomTechnologyJourney>();
    services.AddScoped<CreateEquipmentJourney, CreateEquipmentJourney>();
    services.AddScoped<CreateSkillJourney, CreateSkillJourney>();
    services.AddScoped<CreateUnitJourney, CreateUnitJourney>();



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