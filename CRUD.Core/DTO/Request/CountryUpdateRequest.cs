namespace CRUD.Core.DTO.Request;

public class CountryUpdateRequest
{
    public Guid CountryId { get; set; }
    public required string Name { get; set; }
}