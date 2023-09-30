using CRUD.Core.DTO.Request;
using CRUD.Core.DTO.Response;

namespace CRUD.Infrastructure.RepositoryContracts;

public interface IPersonRepository
{
    Task<PersonResponse> PostPersonAsync(PersonAddRequest request);
    Task<List<PersonResponse>> GetAllPersonsAsync();
    Task<PersonResponse> GetPersonByIDAsync(Guid id);
    Task<PersonResponse> PutPersonAsync(Guid id, PersonAddRequest request);
    Task<bool> DeletePersonAsync(Guid id);
}
