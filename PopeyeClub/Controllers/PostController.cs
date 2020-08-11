using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace PopeyeClub.Controllers
{
    [Authorize]
    public class PostController : Controller
    {
        public IActionResult Overview()
        {
            return View();
        }
    }
}
