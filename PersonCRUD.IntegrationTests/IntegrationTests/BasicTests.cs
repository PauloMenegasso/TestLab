using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc.Testing;
using PersonCRUD.Domain;

namespace PersonCRUD.IntegrationTests;

public class BasicTests : IClassFixture<CustomWebApplicationFactory<Program>>
{
    private readonly CustomWebApplicationFactory<Program> _factory;
    public BasicTests(CustomWebApplicationFactory<Program> factory)
    {
        _factory = factory;
    }

    [Fact]
    public async Task when_calling_delete_endpoint_should_return_status_200()
    {
        //Arrange
        var client = _factory.CreateClient();
        await InsertValidPerson(client);

        //Act
        var response = await client.DeleteAsync("/person/1");

        //Assert
        response.EnsureSuccessStatusCode();
    }

    [Fact]
    public async Task when_inserting_an_invalid_person_should_get()
    {
        //Arrange
        var client = _factory.CreateClient();
        await InsertValidPerson(client);

        //Act
        var response = await client.DeleteAsync("/person/1");

        //Assert
        response.EnsureSuccessStatusCode();
    }

    [Fact]
    public async Task when_calling_get_all_endpoint_should_return_status_200()
    {
        //Arrange
        var client = _factory.CreateClient();
        await InsertValidPerson(client);

        //Act
        var response = await client.GetAsync("/person");

        //Assert
        response.EnsureSuccessStatusCode();
    }

    private async Task InsertValidPerson(HttpClient client)
    {
        var person = new Person { Name = "Test", Age = 20, Email = "123@321.com" };
        await client.PostAsJsonAsync("/person", person);
    }
}