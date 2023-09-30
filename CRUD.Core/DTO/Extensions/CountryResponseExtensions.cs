using CRUD.Core.Domain.Entities;
using CRUD.Core.DTO.Response;

namespace CRUD.Core.DTO.Extensions;
public static class CountryResponseExtensions
{
    public static CountryResponse ToCountryResponse(this Country country) => new()
    {
        Name = country.Name,
        CountryId = country.CountryId
    };

}
