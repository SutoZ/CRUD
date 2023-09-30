namespace CRUD.Core.DTO.Request;

public class CountryGetRequest
{
    public Guid CountryId { get; set; }
    public required string Name { get; set; }
}
