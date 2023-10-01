using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace CRUD.Core.Domain.Entities;

public class Person
{
    [Key]
    public Guid PersonId { get; set; }

    [Required]
    public required string Name { get; set; }
    public string? Email { get; set; }

    [DefaultValue(false)]
    public bool? Gender { get; set; }
    public Guid? CountryId { get; set; }
    public string? Address { get; set; }
    public DateTime? DateOfBirth { get; set; }
    public string? PersonalID { get; set; }
    public Country? Country { get; set; }
}
