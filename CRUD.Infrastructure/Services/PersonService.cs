using CRUD.Core.DTO.Request;
using CRUD.Core.DTO.Response;
using CRUD.Core.ServiceContracts;
using CRUD.Infrastructure.RepositoryContracts;
using CRUD.Infrastructure.ServiceContracts;

namespace CRUD.Infrastructure.Services;

public class PersonService : IPersonService
{
    private readonly ICountryService __countryService;
    private readonly IPersonRepository __personRepository;

    public PersonService(ICountryService countryService, IPersonRepository personRepository)
    {
        __countryService = countryService;
        __personRepository = personRepository;
    }

    public async Task<List<PersonResponse>> GetAllPersonsAsync() => await __personRepository.GetAllPersonsAsync();
    public async Task<PersonResponse> GetPersonByIDAsync(Guid id) => await __personRepository.GetPersonByIDAsync(id);
    public async Task<PersonResponse> PostPersonAsync(PersonAddRequest request) => await __personRepository.PostPersonAsync(request);
    public async Task<PersonResponse> PutPersonAsync(Guid id, PersonUpdateRequest request) => await __personRepository.PutPersonAsync(id, request);
    public async Task<bool> DeletePersonAsync(Guid id) => await __personRepository.DeletePersonAsync(id);
}
