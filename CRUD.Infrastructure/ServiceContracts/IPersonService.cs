using CRUD.Core.DTO.Request;
using CRUD.Core.DTO.Response;

namespace CRUD.Infrastructure.ServiceContracts
{
    public interface IPersonService
    {
        Task<PersonResponse> PostPersonAsync(PersonAddRequest request);
        Task<PersonResponse> PutPersonAsync(Guid id, PersonUpdateRequest request);
        Task<List<PersonResponse>> GetAllPersonsAsync();
        Task<PersonResponse> GetPersonByIdAsync(Guid id);
        Task<bool> DeletePersonAsync(Guid id);
    }
}