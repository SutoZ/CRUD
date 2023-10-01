using System.Linq.Expressions;
using CRUD.Core.Domain.Entities;
using CRUD.Core.DTO.Request;
using CRUD.Core.DTO.Response;

namespace CRUD.Infrastructure.RepositoryContracts;

public interface IPersonRepository
{
    Task<PersonResponse> PostPersonAsync(PersonAddRequest request);
    Task<List<PersonResponse>> GetAllPersonsAsync();
    Task<PersonResponse?> GetPersonByIdAsync(Guid id);
    Task<PersonResponse> PutPersonAsync(Guid id, PersonUpdateRequest request);
    Task<bool> DeletePersonAsync(Guid id);
    Task<IEnumerable<Person>> GetFilteredPerson(Expression<Func<Person, bool>> predicate);
}
