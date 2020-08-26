using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PopeyeClub.Services.Interfaces;

namespace PopeyeClub.Controllers
{
    [Authorize]
    public class ChatController : Controller
    {
        private readonly IChatService chatService;

        public ChatController(IChatService chatService)
        {
            this.chatService = chatService;
        }

        public IActionResult Overview()
        {
            return View();
        }

        public IActionResult Create(string userId)
        {
            if(userId != default)
            {
                chatService.Create(userId);
            }
            return RedirectToAction(nameof(Overview));
        }
    }
}
