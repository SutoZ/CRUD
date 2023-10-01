namespace CRUD.Core.DTO.Request;
public class PersonUpdateRequest
{
    public Guid PersonId { get; set; }
    public required string Name { get; set; }
    public string? Email { get; set; }
    public Guid? CountryId { get; set; }
}
