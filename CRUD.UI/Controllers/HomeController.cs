using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace CRUD.UI.Controllers
{
    [Route("api/[controller]")]
    public class HomeController : Controller
    {
        [NonAction]
        [Route("Error")]
        public IActionResult Error()
        {
            IExceptionHandlerPathFeature? exceptionHandlerPathFeature =
                HttpContext.Features.Get<IExceptionHandlerPathFeature>();


            if (exceptionHandlerPathFeature != null && exceptionHandlerPathFeature.Error != null)
            {
                ViewBag.ErrorMessage = exceptionHandlerPathFeature.Error.Message;
            }

            return View();
        }
    }
}
