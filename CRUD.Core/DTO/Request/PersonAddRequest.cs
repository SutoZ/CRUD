namespace CRUD.Core.DTO.Request;

public class PersonAddRequest
{
    public Guid PersonId { get; set; }
    public required string Name { get; set; }
    public string? Email { get; set; }
    public bool? Gender { get; set; }
    public Guid? CountryId { get; set; }
    public string? Address { get; set; }
    public DateTime? DateOfBirth { get; set; }
}
