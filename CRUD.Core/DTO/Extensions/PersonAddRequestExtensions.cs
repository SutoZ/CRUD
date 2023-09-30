using CRUD.Core.Domain.Entities;
using CRUD.Core.DTO.Request;

namespace CRUD.Core.DTO.Extensions;
public static class PersonAddRequestExtensions
{
    public static Person ToPersonObject(this PersonAddRequest request) => new()
    {
        Name = request.Name,
        Email = request.Email,
        Address = request.Address,
        CountryId = request.CountryId,
        DateOfBirth = request.DateOfBirth,
        Gender = request.Gender,
        PersonId = request.PersonId
    };
}