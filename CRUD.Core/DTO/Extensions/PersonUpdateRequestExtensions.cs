using CRUD.Core.Domain.Entities;
using CRUD.Core.DTO.Request;

namespace CRUD.Core.DTO.Extensions;
public static class PersonUpdateRequestExtensions
{
    public static Person ToPersonUpdateRequest(this PersonUpdateRequest person) => new()
    {
        Name = person.Name,
        Email = person.Email
    };
}