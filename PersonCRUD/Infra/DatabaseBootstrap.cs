using Dapper;
using Microsoft.Data.Sqlite;

namespace PersonCRUD.Infra;

public interface IDatabaseBootstrap
{
    void Setup();
}

public class DatabaseBootstrap : IDatabaseBootstrap
{
    private readonly DatabaseConfig _config;

    public DatabaseBootstrap(DatabaseConfig config)
    {
        _config = config;
    }
    public void Setup()
    {
        using var connection = new SqliteConnection(_config.Name);

        var personTable = connection.Query<string>("SELECT name FROM sqlite_master WHERE type='table' AND name='Person';");
        var personTableName = personTable.FirstOrDefault();

        if (string.IsNullOrEmpty(personTableName))
        {
            connection.Execute(@"CREATE TABLE Person(
                PersonId INTEGER PRIMARY KEY AUTOINCREMENT,
                Name VARCHAR(100) not null,
                Age INT not null,
                Email VARCHAR(120) not null);");
        }
    }
}