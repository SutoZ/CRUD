using CRUD.Core.Domain.Entities;
using CRUD.Core.DTO.Request;

namespace CRUD.Core.DTO.Extensions;
public static class CountryAddRequestExtensions
{
    public static Country ToCountryObject(this CountryAddRequest country) => new()
    {
        Name = country.Name,
        CountryId = country.CountryId
    };
}