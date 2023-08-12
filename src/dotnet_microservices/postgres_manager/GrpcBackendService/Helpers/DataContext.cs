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

    public DataContext(IConfiguration configuration)
    {
        Configuration = configuration;
        var credentialsSection = configuration.GetSection("Credentials");
        _host = credentialsSection.GetSection("Host").Value;
        _port = credentialsSection.GetSection("Port").Value;
        _user = credentialsSection.GetSection("User").Value;
        _dbName = credentialsSection.GetSection("DbName").Value;
        _password = credentialsSection.GetSection("password").Value;

    }

    public IDbConnection CreateConnection()
    {
        string connString =
                String.Format(
                    "Server={0};Username={1};Database={2};Port={3};Password={4};SSLMode=Prefer",
                    _host,
                    _user,
                    _dbName,
                    _port,
                    _password);


        return new NpgsqlConnection(connString);
    }


    public void Init()
    {
        // create database tables if they don't exist
        using var connection = CreateConnection();

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
            //config.AddMap(new TechnologyMap());
            config.AddConvention<PropertyTransformConvention>().ForEntity<Technology>().ForEntity<KingdomTechnology>();
        });
    }
}
