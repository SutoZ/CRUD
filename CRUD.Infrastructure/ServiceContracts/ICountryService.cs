﻿using CRUD.Core.DTO.Request;
using CRUD.Core.DTO.Response;

namespace CRUD.Infrastructure.ServiceContracts;

public interface ICountryService
{
    Task<CountryResponse> PostCountryAsync(CountryAddRequest? request);
    Task<bool> DeleteCountryAsync(Guid countryId);
    Task<IEnumerable<CountryResponse>> GetAllCountriesAsync();
    Task<CountryResponse?> GetCountryByIdAsync(Guid? id);
    Task<CountryResponse> PutCountryAsync(Guid id, CountryUpdateRequest request);
}
