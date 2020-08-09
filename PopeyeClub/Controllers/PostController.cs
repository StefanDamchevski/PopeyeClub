using Microsoft.AspNetCore.Mvc;

namespace PopeyeClub.Controllers
{
    public class PostController : Controller
    {
        public IActionResult Overview()
        {
            return View();
        }
    }
}
