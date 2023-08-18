<Query Kind="Statements">
  <Connection>
    <ID>86af301a-9095-43bf-8437-af44453f1291</ID>
    <NamingServiceVersion>2</NamingServiceVersion>
    <Persist>true</Persist>
    <Driver Assembly="(internal)" PublicKeyToken="no-strong-name">LINQPad.Drivers.EFCore.DynamicDriver</Driver>
    <Server>localhost</Server>
    <UserName>postgres</UserName>
    <Password>AQAAANCMnd8BFdERjHoAwE/Cl+sBAAAAHN8B2UGrsUGoirTq7VZY5gAAAAACAAAAAAAQZgAAAAEAACAAAAA+EmgcfQAm7ZlH7KSSGrA5XzfEbGEJEWIW/rL87qOGawAAAAAOgAAAAAIAACAAAACQf5vutNqIb0klTB3GbVeStaH4MFseLDRR00RNOcxSwhAAAAA0u9fyrlGMyf5a3aHPfR+WQAAAACjLh9L2qJuDjkdpFWxWtccYWGu1HKlGS3I68F9Dw1/mnE4ISmwS6smIoKPko5Rmks5tNt6XocFinVVyj4offFY=</Password>
    <Database>aoe</Database>
    <DisplayName>Age of Empires</DisplayName>
    <DriverData>
      <EncryptSqlTraffic>True</EncryptSqlTraffic>
      <PreserveNumeric1>True</PreserveNumeric1>
      <EFProvider>Npgsql.EntityFrameworkCore.PostgreSQL</EFProvider>
    </DriverData>
  </Connection>
  <NuGetReference>Dapper</NuGetReference>
  <NuGetReference>Dapper.FluentMap</NuGetReference>
  <Namespace>Npgsql</Namespace>
  <Namespace>Dapper</Namespace>
</Query>

DateTime.UtcNow.ToString().Dump();

var host = "localhost";
var port = "5432";
var user = "postgres";
var dbName = "aoe";
var password = "docker";

var connectionString = $"Server={host};Username={user};Database={dbName};Port={port};Password={password};SSLMode=Prefer;Include Error Detail=true";

IDbConnection Create() => new NpgsqlConnection(connectionString);


using var connection = Create();

var query = @"SELECT * FROM get_kingdom_units(1);";

connection.Query(query).Dump();


