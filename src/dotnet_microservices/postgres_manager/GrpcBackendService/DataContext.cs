
using Dapper;
using Dapper.FluentMap;
using DataAccessLibrary;
using GrpcBackendService.Models.Mappings;
using Npgsql;
using System.Data;
using System.Reflection;

namespace GrpcBackendService.DataAccess.Repositories;
public sealed class DataContext : IConnectionCreator
{
    private readonly string connectionString;
    private readonly IConfiguration Configuration;
    //public readonly NpgsqlDataSource DataSource;

    public DataContext(IConfiguration configuration)
    {
        Configuration = configuration;
        var credentialsSection = configuration.GetSection("Credentials");
        var host = credentialsSection.GetSection("Host").Value;
        var port = credentialsSection.GetSection("Port").Value;
        var user = credentialsSection.GetSection("User").Value;
        var dbName = credentialsSection.GetSection("DbName").Value;
        var password = credentialsSection.GetSection("Password").Value;

        connectionString = $"Server={host};Username={user};Database={dbName};Port={port};Password={password};SSLMode=Prefer;Include Error Detail=true";

        //var dataSourceBuilder = new NpgsqlDataSourceBuilder(connectionString);
        //dataSourceBuilder.MapEnum<SkillType>("skill_type");
        //DataSource = dataSourceBuilder.Build();
    }

    public IDbConnection Create() => new NpgsqlConnection(connectionString);

    public void Init()
    {
        // create database tables if they don't exist
        using var connection = Create();

        //_executeScript("CreateTablesScriptLocation");
        //_executeScript("CreateFunctionsScriptLocation");
        //_executeScript("InsertDataScriptLocation");

        void _executeScript(string parameterName)
        {
            var filePath = Configuration.GetConnectionString(parameterName);
            var scriptSql = File.ReadAllText(filePath);
            connection.Execute(scriptSql);
        }

        FluentMapper.Initialize(config =>
        {
            //config.AddConvention<PropertyTransformConvention>().ForEntity<Technology>().ForEntity<KingdomTechnology>().ForEntity<TechnologyDependency>().ForEntity<KingdomTransaction>();
            //config.AddConvention<PropertyTransformConvention>().ForEntitiesInCurrentAssembly("DataAccessLibrary.Models");

            config.AddConvention<PropertyTransformConvention>().ForEntitiesInAssembly(Assembly.Load("DataAccessLibrary"));
        });
    }
}
