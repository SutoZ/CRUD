using CRUD.Core.Domain.Entities;
using CRUD.Core.DTO.Request;
using CRUD.Core.DTO.Response;

namespace CRUD.Core.DTO.Extensions;

public static class PersonResponseExtensions
{
    public static PersonResponse ToPersonResponse(this Person person) => new()
    {
        Name = person.Name,
        Address = person.Address,
        CountryId = person.CountryId,
        Email = person.Email,
        DateOfBirth = person.DateOfBirth,
        Gender = person.Gender,
        PersonId = person.PersonId,
        PersonalID = person.PersonalID,
        Country = person.Country?.Name
    };

    public static PersonUpdateRequest ToPersonUpdateRequest(this PersonResponse person) => new()
    {
        Name = person.Name,
        Email = person.Email,
        CountryId = person.CountryId
    };
}