using System.ComponentModel.DataAnnotations;

namespace CRUD.Core.Domain.Entities;

public class Country
{
    [Key]
    public Guid CountryId { get; set; }
    public required string Name { get; set; }
    public virtual ICollection<City> Cities { get; set; }
}
