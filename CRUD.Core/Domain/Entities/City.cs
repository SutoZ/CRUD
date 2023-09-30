using System.ComponentModel.DataAnnotations;

namespace CRUD.Core.Domain.Entities;

public class City
{
 
    [Key]
    public Guid CityId { get; set; }
    public required string Name { get; set; }
    public string? PostalCode { get; set; }
    public int? Population { get; set; }
    public Guid? CountryId { get; set; }
    public Country? Country { get; set; }
}