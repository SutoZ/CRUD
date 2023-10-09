using AutoFixture;
using CRUD.Core.Domain.Entities;
using CRUD.Core.DTO.Extensions;
using CRUD.Core.DTO.Request;
using CRUD.Core.DTO.Response;
using CRUD.Infrastructure;
using CRUD.Infrastructure.RepositoryContracts;
using CRUD.Infrastructure.ServiceContracts;
using CRUD.Infrastructure.Services;
using EntityFrameworkCoreMock;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Moq;
using Xunit.Abstractions;

namespace CRUD.Tests;
public class PersonServiceTests
{
    private readonly ITestOutputHelper _outputHelper;
    private readonly IFixture _fixture;
    private readonly IPersonService _personService;
    private readonly ICountryService _countryService;

    private readonly Mock<IPersonRepository> _personRepositoryMock;
    private readonly Mock<ICountryRepository> _countryRepositoryMock;

    private readonly IPersonRepository _personRepository;
    private readonly ICountryRepository _countryRepository;


    public PersonServiceTests(ITestOutputHelper outputHelper)
    {
        _outputHelper = outputHelper;
        _fixture = new Fixture();
        var personsInitialData = new List<Person>();
        var countriesInitialData = new List<Country>();
        var mockContext = new DbContextMock<ApplicationDbContext>(new DbContextOptionsBuilder<ApplicationDbContext>().Options);
        _personRepositoryMock = new Mock<IPersonRepository>();
        _countryRepositoryMock = new Mock<ICountryRepository>();

        _personRepository = _personRepositoryMock.Object;
        _countryRepository = _countryRepositoryMock.Object;


        var context = mockContext.Object;

        mockContext.CreateDbSetMock(temp => temp.Persons, personsInitialData);
        mockContext.CreateDbSetMock(temp => temp.Countries, countriesInitialData);

        _countryService = new CountryService(_countryRepository);
        _personService = new PersonService(_countryService, _personRepository);

    }

    [Fact]
    public async Task AddPerson_PersonDetails_ShouldBeSuccessful()
    {
        //Arrange
        PersonAddRequest? personAddRequest = _fixture
            .Build<PersonAddRequest>()
            .With(x => x.Email, "mytestemail@gmail.com")
            .Create();

        var response = personAddRequest.ToPersonResponse();

        _personRepositoryMock.Setup(temp =>
            temp.PostPersonAsync(It.IsAny<PersonAddRequest>())).ReturnsAsync(response);


        //Act
        PersonResponse? personResponse_from_Add = await _personService.PostPersonAsync(personAddRequest);
        PersonResponse personResponseExpected = personAddRequest.ToPersonResponse();

        personResponseExpected.PersonId = personResponse_from_Add.PersonId;

        //Assert
        personResponse_from_Add.PersonId.Should().NotBe(Guid.Empty);
        personResponse_from_Add.PersonId.Should().Be(personResponseExpected.PersonId);
    }

    [Fact]
    public async Task AddPerson_NameIsNull()
    {
        //Arrange
        PersonAddRequest? personAddRequest =
            _fixture
                .Build<PersonAddRequest>()
                .With(x => x.Name, null as string)
                .Create();

        PersonResponse personResponse = personAddRequest.ToPersonResponse();
        _personRepositoryMock.Setup(temp => temp.PostPersonAsync(It.IsAny<PersonAddRequest>())).ReturnsAsync(personResponse);

        var action = async () =>
        {
            //Act
            await _personService.PostPersonAsync(personAddRequest);
        };

        //Assert
        _outputHelper.WriteLine("Expected");
        await action.Should().ThrowAsync<ArgumentException>();
    }

    [Fact]
    public async Task GetPersonByPersonId_NullPersonId()
    {
        //Arrange
        Guid? personId = null;

        //Act
        PersonResponse null_respons = await _personService.GetPersonByIdAsync(personId.Value);

        //Assert
        null_respons.Should().BeNull();
    }

    [Fact]
    public async Task GetPersonByPersonId()
    {
        //Arrange
        PersonAddRequest? personAddRequest =
            _fixture
                .Build<PersonAddRequest>()
                .With(x => x.Name, null as string)
                .Create();

        var personsResponseExpected = personAddRequest.ToPersonResponse();
        _personRepositoryMock.Setup(temp => temp.GetPersonByIdAsync(personAddRequest.PersonId))
            .ReturnsAsync(personsResponseExpected);

        //Act
        PersonResponse personResponseFromGetById = await _personService.GetPersonByIdAsync(personAddRequest.PersonId);

        //Assert
        _outputHelper.WriteLine("Expected");
        personResponseFromGetById.Should().Be(personsResponseExpected);
    }

    [Fact]
    public async Task GetAllPersons_EmptyList()
    {
        //Arrange
        List<PersonResponse> personsResponse = new();
        _personRepositoryMock.Setup(temp => temp.GetAllPersonsAsync()).ReturnsAsync(personsResponse);

        //Act
        List<PersonResponse> personsFromGet = await _personService.GetAllPersonsAsync();

        //Assert
        _outputHelper.WriteLine("Expected");
        personsFromGet.Should().BeEmpty();
    }

    [Fact]
    public async Task GetAllPersons_WithFewPersons_ShouldBeSuccessful()
    {
        //Arrange
        List<PersonResponse> personsResponse = new()
        {
            _fixture
                .Build<PersonResponse>()
                .With(x => x.Email, "examleemail@gmail.com")
                .With(x => x.Country, null as CountryResponse)
                .Create(),
            _fixture
                .Build<PersonResponse>()
                .With(x => x.Email, "examleemail@gmail.com")
                .With(x => x.Country, null as CountryResponse)
                .Create(),
            _fixture
                .Build<PersonResponse>()
                .With(x => x.Email, "examleemail@gmail.com")
                .With(x => x.Country, null as CountryResponse)
                .Create()
        };

        _personRepositoryMock.Setup(temp => temp.GetAllPersonsAsync()).ReturnsAsync(personsResponse);

        //Act
        List<PersonResponse> personResponsesExpected = await _personService.GetAllPersonsAsync();

        //Asert
        personsResponse.Should().BeEquivalentTo(personResponsesExpected);
    }

    [Fact]
    public async Task UpdatePerson_InvalidPersonId()
    {
        //Arrange
        PersonUpdateRequest? addPerson = _fixture
            .Build<PersonUpdateRequest>()
            .Create();

        Guid? personGuid = null;


        Func<Task> action = async () =>
        {
            //Act
            await _personService.PutPersonAsync(personGuid.Value, addPerson);
        };

        //Assert
        action.Should().ThrowAsync<ArgumentException>();
    }

    [Fact]
    public async Task UpdatePerson_PersonDetailsUpdate()
    {
        //Arrange
        var countryAddRequest = _fixture.Build<CountryAddRequest>().Create();

        var countryResponse = await _countryService.PostCountryAsync(countryAddRequest);

        var personAddRequest = _fixture.Build<PersonAddRequest>().Create();

        PersonResponse personResponse_from_add = await _personService.PostPersonAsync(personAddRequest);

        PersonUpdateRequest personUpdateRequest = personResponse_from_add.ToPersonUpdateRequest();
        personUpdateRequest.Name = "TestName";
        personUpdateRequest.Email = "testemail@gmail.com";

        //Act
        PersonResponse personResponse_from_Update =
            await _personService.PutPersonAsync(Guid.NewGuid(), personUpdateRequest);

        PersonResponse personResponse_from_get =
            await _personService.GetPersonByIdAsync(personResponse_from_Update.PersonId);
        //Assert

        personResponse_from_get.PersonId.Should().Be(personResponse_from_Update.PersonId);
    }

    [Fact]
    public async Task DeletePerson_InvalidId()
    {
        //Act
        bool deletePerson = await _personService.DeletePersonAsync(Guid.NewGuid());

        //Assert
        deletePerson.Should().BeFalse();
    }
}
