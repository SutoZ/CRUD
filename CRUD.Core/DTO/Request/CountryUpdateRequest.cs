using CRUD.Core.DTO.Response;

namespace CRUD.Core.DTO.Request;

public class CountryUpdateRequest
{
    public Guid CountryId { get; set; }
    public required string Name { get; set; }
}

public static class CountryUpdateRequestExtensions
{
    public static CountryResponse ToCountryResponse(this CountryUpdateRequest request) => new()
    {
        Name = request.Name,
        CountryId = request.CountryId
    };
}