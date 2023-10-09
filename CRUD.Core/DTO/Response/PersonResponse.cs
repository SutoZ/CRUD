namespace CRUD.Core.DTO.Response;

public class PersonResponse
{
    public Guid PersonId { get; set; }
    public required string Name { get; set; }
    public string? Email { get; set; }
    public bool? Gender { get; set; }
    public CountryResponse? Country { get; set; }
    public Guid? CountryId { get; set; }
    public string? Address { get; set; }
    public DateTime? DateOfBirth { get; set; }
    public string? PersonalID { get; set; }


}