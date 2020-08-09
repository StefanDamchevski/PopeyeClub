using Microsoft.AspNetCore.Mvc;
using PopeyeClub.Services.Dto;
using PopeyeClub.Services.Interfaces;
using PopeyeClub.ViewModels.Auth;
using System.Threading.Tasks;

namespace PopeyeClub.Controllers
{
    public class AuthController : Controller
    {
        private readonly IAuthService authService;

        public AuthController(IAuthService authService)
        {
            this.authService = authService;
        }

        public IActionResult Login()
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Overview", "Post");
            }
            else
            {
                LoginViewModel model = new LoginViewModel();
                return View(model);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                Response response = await authService.SignInAsync(model.Email, model.Password);

                if (response.Succeeded)
                {
                    return RedirectToAction("Overview", "Post");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, response.ErrorMessage);
                    return View(model);
                }
            }
            return View(model);
        }

        public async Task<IActionResult> Logout()
        {
            await authService.SignOutAsync();
            return RedirectToAction(nameof(Login));
        }
    }
}
