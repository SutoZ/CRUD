using CRUD.Core.DTO.Request;
using CRUD.Core.DTO.Response;
using CRUD.Infrastructure.ServiceContracts;
using Microsoft.AspNetCore.Mvc;
using OneOf.Types;
using Swashbuckle.AspNetCore.Annotations;

namespace CRUD.UI.Controllers;


[SwaggerTag("Api definition for Persons")]
public class PersonsController : CustomControllerBase
{
    private readonly IPersonService __personService;
    private readonly ICountryService __countryService;
    private readonly ILogger<PersonsController> _logger;

    public PersonsController(IPersonService personService, ICountryService countryService, ILogger<PersonsController> logger)
    {
        __personService = personService;
        __countryService = countryService;
        _logger = logger;
    }

    [HttpGet]
    [SwaggerOperation("Gets all person elements.")]
    [SwaggerResponse(StatusCodes.Status200OK, "Get Persons was successful")]
    [SwaggerResponse(StatusCodes.Status400BadRequest, "BAD REQUEST")]
    public async Task<IActionResult> Get()
    {
        _logger.LogInformation($"Index action method of {nameof(PersonsController)}");
        var response = await __personService.GetAllPersonsAsync();
        return Ok(response);
    }

    [HttpGet("{id}")]
    [SwaggerOperation("Gets a person element by ID.")]
    [SwaggerResponse(StatusCodes.Status200OK, "Get person by ID was successful")]
    [SwaggerResponse(StatusCodes.Status400BadRequest, "BAD REQUEST")]
    public async Task<ActionResult<PersonResponse>> GetPersonById([FromBody] Guid id)
    {
        var response = await __personService.GetPersonByIdAsync(id);
        return response == null ? NotFound() : Ok(response);
    }

    [HttpPost("{request}")]
    [SwaggerOperation("Creates a person element.")]
    [SwaggerResponse(StatusCodes.Status201Created, "Post person was successful")]
    [SwaggerResponse(StatusCodes.Status400BadRequest, "BAD REQUEST")]
    [HttpPost()]
    public async Task<IActionResult> PostPerson([FromBody] PersonAddRequest request)
    {
        var persons = await __personService.PostPersonAsync(request);
        return CreatedAtAction("GetPersonById", new { id = request.PersonId }, persons);
    }

    [HttpPut("{id}")]
    [SwaggerOperation("Creates a person element.")]
    [SwaggerResponse(StatusCodes.Status201Created, "Post person was successful")]
    [SwaggerResponse(StatusCodes.Status400BadRequest, "BAD REQUEST")]
    public async Task<IActionResult> PutPerson(Guid id, [Bind(nameof(request.Name), nameof(request.Email))] PersonUpdateRequest request)
    {

        if (id != request.CountryId) return Problem("Invalid ID", null, StatusCodes.Status400BadRequest, "Update person");   //400

        var existingPerson = await __personService.PutPersonAsync(id, request);
        if (existingPerson == null) return NotFound(); //404

        return NoContent();
    }

    [HttpDelete("{id}")]
    [SwaggerOperation("Deletes a person element.")]
    [SwaggerResponse(StatusCodes.Status201Created, "Delete person was successful")]
    [SwaggerResponse(StatusCodes.Status400BadRequest, "BAD REQUEST")]
    public async Task<IActionResult> DeletePerson(Guid id)
    {
        var existingPerson = await __personService.DeletePersonAsync(id);
        if (existingPerson == null) return NotFound(); //404

        return NoContent();
    }
}