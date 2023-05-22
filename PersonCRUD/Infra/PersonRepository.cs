using PersonCRUD.Domain;
using Dapper;
using Microsoft.Data.Sqlite;

namespace PersonCRUD.Infra;

public interface IPersonRepository
{
    Task<Person> Get(int id);
    Task<IEnumerable<Person>> Get();
    Task Insert(Person person);
    Task Update(Person person);
    Task Delete(int id);
    Task<Person> GetByEmail(string email);
}

public class PersonRepository : IPersonRepository
{
    private readonly DatabaseConfig _config;
    public PersonRepository(DatabaseConfig config)
    {
        _config = config;
    }

    private SqliteConnection GetConnection()
    {
        return new SqliteConnection(_config.Name);
    }

    public async Task Delete(int id)
    {
        var conn = GetConnection();
        await conn.DeleteAsync<Person>(id);
    }

    public async Task<Person> Get(int id)
    {
        var conn = GetConnection();

        var person = await conn.GetAsync<Person>(id);

        return person;
    }

    public async Task<IEnumerable<Person>> Get()
    {
        var conn = GetConnection();

        var people = await conn.GetListAsync<Person>();

        return people;
    }

    public async Task Insert(Person person)
    {
        var conn = GetConnection();
        //  await conn.InsertAsync<Person>(person);
        await conn.ExecuteAsync("Insert into Person (Name, Age, Email) VALUES (@Name, @Age, @Email);", person);
    }

    public async Task Update(Person person)
    {
        var conn = GetConnection();
        await conn.UpdateAsync<Person>(person);
    }

    public async Task<Person> GetByEmail(string email)
    {
        var conn = GetConnection();

        var person = (await conn.QueryAsync<Person>($"Select * from Person where Email = '{email}'")).ToList();

        return person.FirstOrDefault();
    }
}