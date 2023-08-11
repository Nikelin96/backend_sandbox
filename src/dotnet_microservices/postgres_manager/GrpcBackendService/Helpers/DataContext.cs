using Dapper;
using Dapper.FluentMap;
using GrpcBackendService.Models;
using GrpcBackendService.Models.Mappings;
using Npgsql;
using System.Data;

namespace GrpcBackendService.Helpers;

public sealed class DataContext
{
    private static string Host = "localhost";
    private static string User = "postgres";
    private static string DBname = "aoe";
    private static string Password = "docker";
    private static string Port = "5432";

    protected readonly IConfiguration Configuration;

    public DataContext(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    public IDbConnection CreateConnection()
    {
        string connString =
                String.Format(
                    "Server={0};Username={1};Database={2};Port={3};Password={4};SSLMode=Prefer",
                    Host,
                    User,
                    DBname,
                    Port,
                    Password);


        return new NpgsqlConnection(connString);
    }


    public async Task Init()
    {
        // create database tables if they don't exist
        using var connection = CreateConnection();

        await _executeScript("CreateScriptLocation");
        //await _executeScript("InsertScriptLocation");

        async Task _executeScript(string parameterName)
        {
            var filePath = Configuration.GetConnectionString(parameterName);
            var scriptSql = File.ReadAllText(filePath);
            await connection.ExecuteAsync(scriptSql);
        }

        FluentMapper.Initialize(config =>
        {
            //config.AddMap(new TechnologyMap());
            config.AddConvention<PropertyTransformConvention>().ForEntity<Technology>();
        });
    }
}
