using CRUD.Core.DTO.Request;
using CRUD.Core.DTO.Response;
using CRUD.Infrastructure.RepositoryContracts;
using CRUD.Infrastructure.ServiceContracts;

namespace CRUD.Infrastructure.Services;

public class CountryService : ICountryService
{
    private readonly ICountryRepository __countryRepository;

    public CountryService(ICountryRepository countryRepository)
    {
        __countryRepository = countryRepository;
    }

    public async Task<IEnumerable<CountryResponse>> GetAllCountriesAsync() => await __countryRepository.GetAllCountriesAsync();
    public async Task<CountryResponse?> GetCountryByIdAsync(Guid? id) => await __countryRepository.GetCountryByIdAsync(id);
    public async Task<CountryResponse> PostCountryAsync(CountryAddRequest? request) => await __countryRepository.PostCountryAsync(request);
    public async Task<bool> DeleteCountryAsync(Guid countryId) => await __countryRepository.DeleteCountryAsync(countryId);
    public async Task<CountryResponse> PutCountryAsync(Guid id, CountryUpdateRequest request)
        => await __countryRepository.PutCountryAsync(id, request);

}