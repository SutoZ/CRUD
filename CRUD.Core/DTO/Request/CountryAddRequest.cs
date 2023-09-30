namespace CRUD.Core.DTO.Request;

public class CountryAddRequest
{
    public Guid CountryId { get; set; }
    public required string Name { get; set; }
}
