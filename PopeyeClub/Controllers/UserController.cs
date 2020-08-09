using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PopeyeClub.Data;
using PopeyeClub.Helpers;
using PopeyeClub.Services.Dto;
using PopeyeClub.Services.Interfaces;
using PopeyeClub.ViewModels.User;
using System.Security.Claims;
using System.Threading.Tasks;

namespace PopeyeClub.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserService userService;

        public UserController(IUserService userService)
        {
            this.userService = userService;
        }

        public IActionResult SignUp()
        {
            SignUpViewModel model = new SignUpViewModel();
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> SignUp(SignUpViewModel model, string profilePicture)
        {
            if (ModelState.IsValid)
            {
                Response response = await userService.CreateAsync(model.Email, model.UserName, model.Password, profilePicture);

                if (response.Succeeded)
                {
                    return RedirectToAction("Login", "Auth");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, response.ErrorMessage);
                }
            }
            return View(model);
        }

        [Authorize]
        public async Task<IActionResult> Profile(string userId)
        {
            ApplicationUser user = await userService.GetByIdAsync(userId);
            return View(user.ToProfileViewModel());
        }

        [Authorize]
        public async Task<IActionResult> Edit(string userId)
        {
            if (User.FindFirst(ClaimTypes.NameIdentifier).Value == userId)
            {
                ApplicationUser user = await userService.GetByIdAsync(userId);
                return View(user.ToEditViewModel());
            }
            else
            {
                return RedirectToAction("Overview", "Post");
            }
        }

        [HttpPost]
        public async Task<IActionResult> Edit(EditProfileViewModel model, IFormFile profilePicture)
        {
            if (ModelState.IsValid)
            {
                Response response = await userService.UpdateAsync(model.UserId, model.Email, model.Phone, model.Username, profilePicture.ToByteArray());

                if (response.Succeeded)
                {
                    return RedirectToAction(nameof(Profile), new { UserId = model.UserId });
                }
                else
                {
                    ModelState.AddModelError(string.Empty, response.ErrorMessage);
                }
            }
            return View(model);
        }

        [Authorize]
        public IActionResult ChangePassword(string userId)
        {
            if (User.FindFirst(ClaimTypes.NameIdentifier).Value == userId)
            {
                ChangePasswordViewModel model = new ChangePasswordViewModel();
                model.UserId = userId;
                return View(model);
            }
            else
            {
                return RedirectToAction("Overview", "Post");
            }
        }

        [HttpPost]
        public async Task<IActionResult> ChangePassword(ChangePasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                Response response = await userService.ChangePasswordAsync(model.UserId, model.OldPassword, model.NewPassword);
                if (response.Succeeded)
                {
                    return RedirectToAction("Logout", "Auth");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, response.ErrorMessage);
                }
            }
            return View(model);
        }
    }
}