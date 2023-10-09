using CRUD.Core.DTO.Request;
using CRUD.Core.DTO.Response;
using CRUD.Infrastructure.ServiceContracts;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace CRUD.UI.Controllers;


[SwaggerTag("Api definition for Countries")]
public class CountriesController : CustomControllerBase
{
    private readonly ICountryService __countryService;

    public CountriesController(ICountryService countryService)
    {
        __countryService = countryService;
    }

    /// <returns>Country elements</returns>
    /// <response code="200">Returns country elements</response>
    /// <response code="400">If request was not successful</response>
    [HttpGet]
    [SwaggerOperation("Gets all country elements.")]
    [SwaggerResponse(StatusCodes.Status200OK, "Get countries was successful")]
    [SwaggerResponse(StatusCodes.Status400BadRequest, "BAD REQUEST")]
    public async Task<IActionResult> Get()
    {
        var response = await __countryService.GetAllCountriesAsync();
        return Ok(response);
    }

    /// <returns>Country element</returns>
    /// <response code="200">Returns country element</response>
    /// <response code="400">If request was not successful</response>
    [HttpGet("{id}")]
    [SwaggerOperation("Gets a country element by ID.")]
    [SwaggerResponse(StatusCodes.Status200OK, "Get country by ID was successful")]
    [SwaggerResponse(StatusCodes.Status400BadRequest, "BAD REQUEST")]
    public async Task<ActionResult<CountryResponse?>> GetCountryById([FromQuery] Guid id)
    {
        var response = await __countryService.GetCountryByIdAsync(id);
        return response == null ? NotFound() : Ok(response);
    }


    /// <returns>Country element</returns>
    /// <response code="201">Returns country element</response>
    /// <response code="400">If request was not successful</response>
    [HttpPost("{request}")]
    [SwaggerOperation("Creates a country element.")]
    [SwaggerResponse(StatusCodes.Status201Created, "Post country was successful")]
    [SwaggerResponse(StatusCodes.Status400BadRequest, "BAD REQUEST")]
    [HttpPost]
    public async Task<IActionResult> PostCountry([FromBody] CountryAddRequest request)
    {
        var countries = await __countryService.PostCountryAsync(request);
        return CreatedAtAction("GetCountryById", new { id = request.CountryId }, countries);
    }


    /// <returns>Country element</returns>
    /// <response code="201">Returns country element</response>
    /// <response code="400">If request was not successful</response>
    [HttpPut("{id}")]
    [SwaggerOperation("Creates a country element.")]
    [SwaggerResponse(StatusCodes.Status201Created, "Put country was successful")]
    [SwaggerResponse(StatusCodes.Status400BadRequest, "BAD REQUEST")]
    public async Task<IActionResult> PutCountry(Guid id, [Bind(nameof(request.Name), nameof(request.CountryId))] CountryUpdateRequest request)
    {
        if (id != request.CountryId) return Problem("Invalid ID", null, StatusCodes.Status400BadRequest, "Update country");   //400

        var existingCountry = await __countryService.PutCountryAsync(id, request);
        if (existingCountry == null) return NotFound(); //404

        return NoContent();
    }


    /// <returns>Country element</returns>
    /// <response code="201">Removes a country element</response>
    /// <response code="400">If request was not successful</response>
    [HttpDelete("{id}")]
    [SwaggerOperation("Deletes a country element.")]
    [SwaggerResponse(StatusCodes.Status201Created, "Delete country was successful")]
    [SwaggerResponse(StatusCodes.Status400BadRequest, "BAD REQUEST")]
    public async Task<IActionResult> DeleteCountry(Guid id)
    {
        var existingCountry = await __countryService.DeleteCountryAsync(id);
        if (existingCountry == null) return NotFound(); //404

        return NoContent();
    }
}
