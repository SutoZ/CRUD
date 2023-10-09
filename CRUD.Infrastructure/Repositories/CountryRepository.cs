using CRUD.Core.DTO.Request;
using CRUD.Core.DTO.Response;
using Microsoft.EntityFrameworkCore;
using System.Data;
using CRUD.Infrastructure.RepositoryContracts;
using CRUD.Core.DTO.Extensions;

namespace CRUD.Infrastructure.Repositories;

public class CountryRepository : ICountryRepository
{
    private readonly ApplicationDbContext __context;

    public CountryRepository(ApplicationDbContext context)
    {
        __context = context;
    }

    public async Task<IEnumerable<CountryResponse>> GetAllCountriesAsync()
    {
        return await __context.Countries
            .Include(x => x.Cities)
            .OrderBy(x => x.Name)
            .Select(y => y.ToCountryResponse())
            .ToListAsync();
    }

    public async Task<CountryResponse?> GetCountryByIdAsync(Guid? id)
    {
        var country = await __context.Countries.FindAsync(id);
        return country?.ToCountryResponse();
    }

    public async Task<CountryResponse> PostCountryAsync(CountryAddRequest request)
    {
        ArgumentNullException.ThrowIfNull(request);

        if (await __context.Countries.CountAsync(x => x.CountryId == request.CountryId) > 0) throw new ArgumentException("Country is already added to db.");

        var country = request.ToCountryObject();

        __context.Countries.Add(country);

        try
        {
            await __context.SaveChangesAsync();
        }
        catch (DBConcurrencyException e)
        {
            throw e;
        }

        return country.ToCountryResponse();
    }

    public async Task<CountryResponse> PutCountryAsync(Guid id, CountryUpdateRequest request)
    {
        ArgumentNullException.ThrowIfNull(request);

        var existingCountry = await __context.Countries.FindAsync(id);

        if (existingCountry == null) return null;


        CountryResponse countryResponse = existingCountry.ToCountryResponse();
       countryResponse = request.ToCountryResponse();

        try
        {
            await __context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException e)
        {
            if (!CountryExists(id))
                throw;
        }

        return countryResponse == null ? null : countryResponse;
    }

    private bool CountryExists(Guid id)
    {
        var existingCountry = __context.Countries.Find(id);
        return existingCountry != null;
    }

    public async Task<bool> DeleteCountryAsync(Guid id)
    {
        var existingCountry = await __context.Countries.FindAsync(id);
        if (existingCountry == null) return false;

        __context.Countries.Remove(existingCountry);

        try
        {
           var deletedRows =  await __context.SaveChangesAsync();
            return deletedRows > 0;
        }
        catch (DBConcurrencyException e)
        {
            throw;
        }
    }
}
