using System.Net;
using CRUD.Core.DTO.Request;
using CRUD.Core.DTO.Response;
using CRUD.Core.ServiceContracts;
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

    public PersonsController(IPersonService personService, ICountryService countryService)
    {
        __personService = personService;
        __countryService = countryService;
    }

    /// <returns>Person elements</returns>
    /// <response code="200">Returns person elements</response>
    /// <response code="400">If request was not successful</response>
    [HttpGet]
    [SwaggerOperation("Gets all person elements.")]
    [SwaggerResponse(StatusCodes.Status200OK, "Get Persons was successful")]
    [SwaggerResponse(StatusCodes.Status400BadRequest, "BAD REQUEST")]
    public async Task<IActionResult> Get()
    {
        var response = await __personService.GetAllPersonsAsync();
        return Ok(response);
    }


    /// <returns>Person elements</returns>
    /// <response code="200">Returns person elements</response>
    /// <response code="400">If request was not successful</response>
    [HttpGet("{id}")]
    [SwaggerOperation("Gets a person element by ID.")]
    [SwaggerResponse(StatusCodes.Status200OK, "Get person by ID was successful")]
    [SwaggerResponse(StatusCodes.Status400BadRequest, "BAD REQUEST")]
    public async Task<ActionResult<PersonResponse>> GetPersonById([FromBody] Guid id)
    {
        var response = await __personService.GetPersonByIDAsync(id);
        return response == null ? NotFound() : Ok(response);
    }


    /// <returns>Person element</returns>
    /// <response code="201">Returns person element</response>
    /// <response code="400">If request was not successful</response>
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


    /// <returns>Person element</returns>
    /// <response code="201">Returns person element</response>
    /// <response code="400">If request was not successful</response>
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


    /// <returns>Person element</returns>
    /// <response code="201">Removes person element</response>
    /// <response code="400">If request was not successful</response>
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