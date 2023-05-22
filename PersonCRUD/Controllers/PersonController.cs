using Microsoft.AspNetCore.Mvc;
using PersonCRUD.Domain;
using PersonCRUD.Services;

namespace PersonCRUD.Controller;

[ApiController]
[Route("[controller]")]
public class PersonController : ControllerBase
{
    private readonly IPersonService _personService;
    public PersonController(IPersonService personService)
    {
        _personService = personService;
    }

    [HttpGet()]
    public async Task<ActionResult> Get()
    {
        var people = await _personService.Get();
        return people.Any() ? Ok(people) : NotFound();
    }

    [HttpGet("{id}")]
    public async Task<ActionResult> Get(int id)
    {
        var person = await _personService.Get(id);
        return person != null ? Ok(person) : NotFound();
    }

    [HttpGet("email/{email}")]
    public async Task<ActionResult> Get(string email)
    {
        var person = await _personService.GetByEmail(email);
        return person != null ? Ok(person) : NotFound();
    }

    [HttpPost()]
    public async Task<ActionResult> Insert([FromBody] Person person)
    {
        await _personService.Insert(person);
        return Ok();
    }

    [HttpPut()]
    public async Task<ActionResult> Update([FromBody] Person person)
    {
        await _personService.Update(person);
        return Ok();
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(int id)
    {
        await _personService.Delete(id);
        return Ok();
    }
}
