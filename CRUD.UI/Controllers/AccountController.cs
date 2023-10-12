using CRUD.Core.Domain.IdentityEntities;
using Microsoft.AspNetCore.Mvc;
using CRUD.Core.DTO.Account;
using Microsoft.AspNetCore.Identity;

namespace CRUD.UI.Controllers
{
    [Route("[controller]/[action]")]
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> _applicationUser;

        public AccountController(UserManager<ApplicationUser> applicationUser)
        {
            _applicationUser = applicationUser;
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterDTO registerDto)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Errors = ModelState.Values.SelectMany(temp => temp.Errors).Select(temp => temp.ErrorMessage);
                return View(registerDto);
            }

            ApplicationUser user = new()
            { Email = registerDto.Email, PhoneNumber = registerDto.Phone, UserName = registerDto.PersonName };

            IdentityResult result = await _applicationUser.CreateAsync(user);

            if (result.Succeeded)
                return RedirectToAction(nameof(PersonsController.Get), "Persons");

            foreach (IdentityError error in result.Errors)
                ModelState.AddModelError("Register", error.Description);

            return View(registerDto);
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }
    }
}
