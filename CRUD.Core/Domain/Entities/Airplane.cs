using System.ComponentModel.DataAnnotations;

namespace CRUD.Core.Domain.Entities;

public class Airplane
{
    [Key]
    public Guid AirplaneId { get; set; }
    public string? Manufacturer { get; set; }
    public string? Type { get; set; }
    public double Price { get; set; }
    public DateTime? Production { get; set; }
}
