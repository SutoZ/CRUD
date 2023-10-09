using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CRUD.Core.Domain.Entities;

public class City
{
    [Key]
    public Guid CityId { get; set; }
    public required string Name { get; set; }
    public string? PostalCode { get; set; }
    public int? Population { get; set; }
    public Guid? CountryId { get; set; }

    [ForeignKey("CountryId")]
    public virtual Country? Country { get; set; }
}