<Query Kind="Statements">
  <NuGetReference>Newtonsoft.Json</NuGetReference>
  <NuGetReference>SQLite</NuGetReference>
  <NuGetReference>System.Data.SQLite</NuGetReference>
  <Namespace>System.Data.SQLite</Namespace>
</Query>

var directoryPath = @"D:\backend_sandbox\src\dotnet_microservices\linqpad_scripts";
var dbPath = directoryPath + @"\automationdb.sqlite";

SQLiteConnection.CreateFile(dbPath);

string connectionString = "Data Source=MyDatabase.sqlite;Version=3;";
SQLiteConnection m_dbConnection = new SQLiteConnection(connectionString);
m_dbConnection.Open();

// varchar will likely be handled internally as TEXT
// the (20) will be ignored
// see https://www.sqlite.org/datatype3.html#affinity_name_examples
string sql = "Create Table highscores (name varchar(20), score int)";
// you could also write sql = "CREATE TABLE IF NOT EXISTS highscores ..."
SQLiteCommand command = new SQLiteCommand(sql, m_dbConnection);
command.ExecuteNonQuery();

sql = "Insert into highscores (name, score) values ('Me', 9001)";
command = new SQLiteCommand(sql, m_dbConnection);
command.ExecuteNonQuery();

m_dbConnection.Close();