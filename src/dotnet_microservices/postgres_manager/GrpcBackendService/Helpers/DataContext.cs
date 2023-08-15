using Dapper;
using Dapper.FluentMap;
using GrpcBackendService.Models;
using GrpcBackendService.Models.Mappings;
using Npgsql;
using System.Data;

namespace GrpcBackendService.Helpers;

public sealed class DataContext
{
    private readonly string _host;
    private readonly string _port;
    private readonly string _user;
    private readonly string _dbName;
    private readonly string _password;

    private readonly IConfiguration Configuration;
    public readonly NpgsqlDataSource DataSource;

    public DataContext(IConfiguration configuration)
    {
        Configuration = configuration;
        var credentialsSection = configuration.GetSection("Credentials");
        _host = credentialsSection.GetSection("Host").Value;
        _port = credentialsSection.GetSection("Port").Value;
        _user = credentialsSection.GetSection("User").Value;
        _dbName = credentialsSection.GetSection("DbName").Value;
        _password = credentialsSection.GetSection("password").Value;

        var connectionString = GetConnectionString();

        var dataSourceBuilder = new NpgsqlDataSourceBuilder(connectionString);
        dataSourceBuilder.MapEnum<SkillType>("skill_type");
        this.DataSource = dataSourceBuilder.Build();
    }

    public IDbConnection CreateConnection()
    {
        var connectionString = GetConnectionString();

        return new NpgsqlConnection(connectionString);
    }

    private string GetConnectionString()
    {
        return $"Server={_host};Username={_user};Database={_dbName};Port={_port};Password={_password};SSLMode=Prefer;Include Error Detail=true";
    }

    public void Init()
    {

        // create database tables if they don't exist
        using var connection = CreateConnection();

        _executeScript("CreateTablesScriptLocation");
        _executeScript("CreateFunctionsScriptLocation");
        _executeScript("InsertDataScriptLocation");

        void _executeScript(string parameterName)
        {
            var filePath = Configuration.GetConnectionString(parameterName);
            var scriptSql = File.ReadAllText(filePath);
            connection.Execute(scriptSql);
        }

        FluentMapper.Initialize(config =>
        {
            config.AddConvention<PropertyTransformConvention>().ForEntity<Technology>().ForEntity<KingdomTechnology>().ForEntity<TechnologyDependency>();
        });
    }
}
