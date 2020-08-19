using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PopeyeClub.Services.Interfaces;
using PopeyeClub.ViewModels.Post;

namespace PopeyeClub.Controllers
{
    [Authorize]
    public class PostSaveController : Controller
    {
        private readonly IPostSaveService postSaveService;

        public PostSaveController(IPostSaveService postSaveService)
        {
            this.postSaveService = postSaveService;
        }

        [HttpPost]
        public IActionResult AddToSaved([FromBody] RequestSaveModel model)
        {
            if (model is null)
            {
                return BadRequest(model);
            }
            else
            {
                string userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
                postSaveService.Create(model.PostId, userId);
                return Ok();
            }
        }

        [HttpPost]
        public IActionResult RemoveFromSaved([FromBody] RequestSaveModel model)
        {
            if (model is null)
            {
                return BadRequest(model);
            }
            else
            {
                string userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
                postSaveService.Update(model.PostId, userId);
                return Ok();
            }
        }
    }
}
