using CRUD.Core.DTO.Request;
using CRUD.Core.DTO.Response;

namespace CRUD.Infrastructure.RepositoryContracts;

public interface ICountryRepository
{
    Task<IEnumerable<CountryResponse>> GetAllCountriesAsync();
    Task<CountryResponse?> GetCountryByIdAsync(Guid? id);
    Task<CountryResponse> PostCountryAsync(CountryAddRequest request);
    Task<CountryResponse> PutCountryAsync(Guid id, CountryUpdateRequest request);
    Task<bool> DeleteCountryAsync(Guid id);
}