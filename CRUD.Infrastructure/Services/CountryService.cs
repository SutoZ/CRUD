using CRUD.Core.Domain.Entities;
using CRUD.Core.DTO.Extensions;
using CRUD.Core.DTO.Request;
using CRUD.Core.DTO.Response;
using CRUD.Core.ServiceContracts;
using CRUD.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace CRUD.Core.Services;

public class CountryService : ICountryService
{
    private readonly ApplicationDbContext __db;

    public CountryService(ApplicationDbContext dbContext)
    {
        __db = dbContext;
    }

    public async Task<List<CountryResponse>> GetAllCountriesAsync()
    {
        var countries = await __db.Countries.Include(x => x.Cities).ToListAsync();
        return countries.Select(x => x.ToCountryResponse()).ToList();
    }

    public async Task<CountryResponse>? GetCountryByIdAsync(Guid? id)
    {
        ArgumentNullException.ThrowIfNull(id);

        Country? country = await __db.Countries.FirstOrDefaultAsync(x => x.CountryId == id);

        return country.ToCountryResponse();
    }

    public async Task<CountryResponse> AddCountryAsync(CountryAddRequest? request)
    {
        ArgumentNullException.ThrowIfNull(request);

        if (request.Name == null) throw new ArgumentNullException(nameof(request));
        if (await __db.Countries.CountAsync(x => x.Name == request.Name) > 0) throw new ArgumentException("Country is already added.");

        Country country = request.ToCountryObject();
        country.CountryId = Guid.NewGuid();

        __db.Add(country);
        await __db.SaveChangesAsync();
        return country.ToCountryResponse();
    }

    public async Task<bool> DeleteCountryAsync(Guid? countryId)
    {
        ArgumentNullException.ThrowIfNull(countryId, nameof(countryId));

        Country? country = await __db.Countries.FirstOrDefaultAsync(x => x.CountryId == countryId);
        if (country == null) return false;

        __db.Countries.Remove(await __db.Countries.FirstAsync(x => x.CountryId == countryId));
        await __db.SaveChangesAsync();

        return true;
    }
}
