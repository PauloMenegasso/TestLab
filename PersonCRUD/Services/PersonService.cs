using PersonCRUD.Domain;
using PersonCRUD.Infra;

namespace PersonCRUD.Services;

public interface IPersonService
{
    Task<Person> Get(int id);
    Task<Person> GetByEmail(string email);
    Task<IEnumerable<Person>> Get();
    Task Insert(Person person);
    Task Update(Person person);
    Task Delete(int id);
    Task<bool> CheckPersonExists(string? email = null, int id = 0);
}

public class PersonService : IPersonService
{
    private readonly IPersonRepository _repository;
    private readonly IloggerService _logger;

    public PersonService(IPersonRepository personRepository, IloggerService logger)
    {
        _repository = personRepository;
        _logger = logger;
    }

    public async Task Delete(int id)
    {
        try
        {
            var existingPerson = await CheckPersonExists(id: id);
            if (!existingPerson)
            {
                throw new Exception("Person not found");
            }
            await _repository.Delete(id);
            _logger.LogInformation($"Person with id {id} deleted");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex);
        }
    }

    public async Task<Person> Get(int id)
    {
        try
        {
            var person = await _repository.Get(id);
            return person;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex);
            return new Person();
        }
    }

    public async Task<IEnumerable<Person>> Get()
    {
        try
        {
            var people = await _repository.Get();
            return people;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex);
            return new List<Person>();
        }
    }

    public async Task<Person> GetByEmail(string email)
    {
        try
        {
            var person = await _repository.GetByEmail(email);
            return person;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex);
            return new Person();
        }
    }

    public async Task Insert(Person person)
    {
        try
        {
            var existingPerson = await CheckPersonExists(person.Email);
            if (existingPerson)
            {
                throw new Exception("Email already inserted!");
            }
            await _repository.Insert(person);
            _logger.LogInformation("Person inserted");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex);
        }
    }

    public async Task Update(Person person)
    {
        try
        {
            var existingPerson = await CheckPersonExists(id: person.PersonId);
            if (!existingPerson)
            {
                throw new Exception("Person not found");
            }
            await _repository.Update(person);
            _logger.LogInformation("Person updated");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex);
        }
    }

    public async Task<bool> CheckPersonExists(string? email = null, int id = 0)
    {
        Person existingPerson;
        if (email != null)
        {
            existingPerson = await _repository.GetByEmail(email);
        }
        else
        {
            existingPerson = await _repository.Get(id);
        }
        return existingPerson != null; //retorna true se a pessoa for encontrada
    }
}
