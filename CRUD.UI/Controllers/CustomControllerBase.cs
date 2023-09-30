using Microsoft.AspNetCore.Mvc;

namespace CRUD.UI.Controllers;

[Route("api/[controller]")]
[ApiController]
[Consumes("application/json")]
[Produces("application/json")]
public class CustomControllerBase : ControllerBase
{

}