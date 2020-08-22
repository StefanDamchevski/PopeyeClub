using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PopeyeClub.Helpers;
using PopeyeClub.Services.Interfaces;
using PopeyeClub.ViewModels.Chat;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;

namespace PopeyeClub.Controllers
{
    [Authorize]
    public class ChatController : Controller
    {
        private readonly IUserService userService;

        public ChatController(IUserService userService)
        {
            this.userService = userService;
        }

        public IActionResult Overview()
        {
            string userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            List<UserViewModel> models = userService.GetAll(userId).Select(x => x.ToUserViewModel()).ToList();
            return View(models);
        }
    }
}
