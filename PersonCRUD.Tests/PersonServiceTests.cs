using AutoBogus;
using NSubstitute;
using PersonCRUD.Domain;
using PersonCRUD.Infra;
using PersonCRUD.Services;

namespace PersonCRUD.Tests.Services;

public class PersonServiceTests
{
    private readonly Random _random;
    private readonly IPersonService _personService;
    private readonly IloggerService _logger;
    private readonly IPersonRepository _repository;
    public PersonServiceTests()
    {
        _random = new Random();
        _logger = Substitute.For<IloggerService>();
        _repository = Substitute.For<IPersonRepository>();

        _personService = new PersonService(_repository, _logger);
    }

    [Fact]
    public async void when_searching_for_user_given_that_user_is_found_should_return_true()
    {
        //Arrange
        var validPerson = MockValidPerson();
        _repository.GetByEmail(Arg.Any<string>()).Returns(validPerson);
        _repository.Get(Arg.Any<int>()).Returns(validPerson);

        //Act
        var result = await _personService.CheckPersonExists();

        //Assert
        Assert.True(result);
    }

    [Fact]
    public async void when_searching_for_user_given_that_email_was_informed_should_call_Get_by_Email()
    {
        //Arrage 
        var email = new AutoFaker<string>().Generate();

        //Act
        await _personService.CheckPersonExists(email);

        //Assert
        await _repository.Received(2).GetByEmail(email);
        await _repository.DidNotReceive().Get(Arg.Any<int>());
    }

    [Fact]
    public async void when_searching_for_user_given_that_id_was_informed_should_call_Get()
    {
        //Arrage 
        var id = _random.Next(0, 100000);

        //Act
        await _personService.CheckPersonExists(id: id);

        //Assert
        await _repository.Received(1).Get(id);
        await _repository.DidNotReceive().GetByEmail(Arg.Any<string>());
    }

    private Person MockValidPerson()
    {
        return new AutoFaker<Person>()
            .RuleFor(p => p.Name, "Test")
            .RuleFor(p => p.Age, _random.Next(18, 90))
            .RuleFor(p => p.Email, "test@test.com")
            .Generate();
    }
}