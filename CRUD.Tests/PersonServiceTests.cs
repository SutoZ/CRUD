using AutoFixture;
using CRUD.Core.Domain.Entities;
using CRUD.Core.DTO.Extensions;
using CRUD.Core.DTO.Request;
using CRUD.Core.DTO.Response;
using CRUD.Core.ServiceContracts;
using CRUD.Infrastructure;
using CRUD.Infrastructure.ServiceContracts;
using CRUD.Infrastructure.Services;
using EntityFrameworkCoreMock;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;

namespace CRUD.Tests;
public class PersonServiceTests
{
    private readonly IFixture __fixture;
    private readonly IPersonService __personService;
    private readonly ICountryService __countryService;

    public PersonServiceTests()
    {
        __fixture = new Fixture();
        var personsInitialData = new List<Person>();
        var mockContext = new DbContextMock<ApplicationDbContext>(new DbContextOptionsBuilder<ApplicationDbContext>().Options);

        ApplicationDbContext context = mockContext.Object;

        mockContext.CreateDbSetMock(temp => temp.Persons, personsInitialData);
    }

    [Fact]
    public async Task AddPerson_PersonDetails()
    {
        //Arrange
        PersonAddRequest? personAddRequest = __fixture
            .Build<PersonAddRequest>()
            .With(x => x.Email, "mytestemail@gmal.com")
            .Create();

        //Act
        PersonResponse? personResponse = await __personService.PostPersonAsync(personAddRequest);

        List<PersonResponse> persons = await __personService.GetAllPersonsAsync();

        //Assert

    }

    [Fact]
    public async Task AddPerson_NameIsNull()
    {
        //Arrange
        PersonAddRequest? personAddRequest =
            __fixture
                .Build<PersonAddRequest>()
                .With(x => x.Name, null as string)
                .Create();

        //Assert
        await Assert.ThrowsAsync<ArgumentException>(async () =>
        {
            //Act
            await __personService.PostPersonAsync(personAddRequest);
        });

    }

    [Fact]
    public async Task GetPersonByPersonId_NullPersonId()
    {
        //Arrange
        Guid? personId = null;

        //Act
        PersonResponse null_respons = await __personService.GetPersonByIDAsync(personId.Value);

        //Assert
        null_respons.Should().BeNull();
    }

    [Fact]
    public async Task GetAllPersons_EmptyList()
    {
        //Arrange
        List<PersonResponse> emptyResponse = await __personService.GetAllPersonsAsync();

        //Assert
        emptyResponse.Should().BeEmpty();
    }

    [Fact]

    public async Task UpdatePerson_InvalidPersonId()
    {
        //Arrange
        PersonUpdateRequest? addPerson = __fixture
            .Build<PersonUpdateRequest>()
            .Create();

        Guid? personGuid = null;

        //Assert
        await Assert.ThrowsAsync<ArgumentException>(async () =>
            //Act
            await __personService.PutPersonAsync(personGuid.Value, addPerson));
    }

    [Fact]
    public async Task UpdatePerson_PersonDetailsUpdate()
    {
        //Arrange
        var countryAddRequest = __fixture.Build<CountryAddRequest>().Create();

        var countryResponse = await __countryService.AddCountryAsync(countryAddRequest);

        var personAddRequest = __fixture.Build<PersonAddRequest>().Create();

        PersonResponse personResponse_from_add = await __personService.PostPersonAsync(personAddRequest);

        PersonUpdateRequest personUpdateRequest = personResponse_from_add.ToPersonUpdateRequest();
        personUpdateRequest.Name = "TestName";
        personUpdateRequest.Email = "testemail@gmail.com";

        //Act
        PersonResponse personResponse_from_Update =
            await __personService.PutPersonAsync(Guid.NewGuid(), personUpdateRequest);

        PersonResponse personResponse_from_get =
            await __personService.GetPersonByIDAsync(personResponse_from_Update.PersonId);
        //Assert

        personResponse_from_get.PersonId.Should().Be(personResponse_from_Update.PersonId);
    }

    [Fact]
    public async Task DeletePerson_InvalidId()
    {
        //Act
        bool deletePerson = await __personService.DeletePersonAsync(Guid.NewGuid());

        //Assert
        deletePerson.Should().BeFalse();
    }
}
