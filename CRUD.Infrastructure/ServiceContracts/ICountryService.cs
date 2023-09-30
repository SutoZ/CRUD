using CRUD.Core.DTO.Request;
using CRUD.Core.DTO.Response;

namespace CRUD.Core.ServiceContracts;

public interface ICountryService
{
    Task<CountryResponse> AddCountryAsync(CountryAddRequest? request);
    Task<bool> DeleteCountryAsync(Guid? countryId);
    Task<List<CountryResponse>> GetAllCountriesAsync();
    Task<CountryResponse>? GetCountryByIdAsync(Guid? id);
}
