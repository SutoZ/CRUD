﻿using System.Data;
using System.Linq.Expressions;
using CRUD.Core.Domain.Entities;
using CRUD.Core.DTO.Response;
using CRUD.Infrastructure.RepositoryContracts;
using Microsoft.EntityFrameworkCore;
using CRUD.Core.DTO.Extensions;
using CRUD.Core.DTO.Request;

namespace CRUD.Infrastructure.Repositories;

public class PersonRepository : IPersonRepository
{
    private readonly ApplicationDbContext __context;

    public PersonRepository(ApplicationDbContext context)
    {
        __context = context;
    }

    public async Task<List<PersonResponse>> GetAllPersonsAsync()
    {
        return await __context.Persons
            .Include(x => x.Country)
            .OrderBy(x => x.Name)
            .Select(y => y.ToPersonResponse())
            .ToListAsync();
    }

    public async Task<PersonResponse?> GetPersonByIdAsync(Guid id)
    {
        var person = await __context.Persons.FindAsync(id);
        return person == null ? null : person.ToPersonResponse();
    }

    public async Task<PersonResponse> PostPersonAsync(PersonAddRequest request)
    {
        ArgumentNullException.ThrowIfNull(request);

        if (await __context.Persons.CountAsync(x => x.PersonId == request.PersonId) > 0) throw new ArgumentException("Person is already added to db.");

        var person = request.ToPersonObject();

        __context.Persons.Add(person);

        try
        {
            await __context.SaveChangesAsync();
        }
        catch (DBConcurrencyException e)
        {
            throw e;
        }

        return person.ToPersonResponse();
    }

    public async Task<PersonResponse> PutPersonAsync(Guid id, PersonUpdateRequest request)
    {
        ArgumentNullException.ThrowIfNull(request);

        var existingPerson = await __context.Persons.FindAsync(id);

        if (existingPerson == null) return null;

        existingPerson.PersonId = request.PersonId;

        try
        {
            await __context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException e)
        {
            if (!PersonExists(id))
                throw e;
        }

        return existingPerson == null ? null : existingPerson.ToPersonResponse();
    }

    private bool PersonExists(Guid id)
    {
        var existingPerson = __context.Persons.Find(id);
        return existingPerson != null;
    }

    public async Task<bool> DeletePersonAsync(Guid id)
    {
        var existingPerson = await __context.Persons.FindAsync(id);
        if (existingPerson == null) return false;

        __context.Persons.Remove(existingPerson);

        try
        {
            var deletedRows = await __context.SaveChangesAsync();
            return deletedRows > 0;
        }
        catch (DBConcurrencyException e)
        {
            throw e;
        }
    }

    public async Task<IEnumerable<Person>> GetFilteredPerson(Expression<Func<Person, bool>> predicate)
    {
        var filteredPersons = await __context.Persons
            .Include(x => x.Country)
            .Where(predicate)
            .ToListAsync();
        return filteredPersons;
    }
}