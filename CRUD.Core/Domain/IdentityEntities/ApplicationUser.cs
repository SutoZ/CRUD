using Microsoft.AspNetCore.Identity;

namespace CRUD.Core.Domain.IdentityEntities;
public class ApplicationUser : IdentityUser<Guid>
{
    public string? UserName { get; set; }
}