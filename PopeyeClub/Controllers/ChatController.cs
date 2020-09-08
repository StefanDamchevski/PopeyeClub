using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using PopeyeClub.Helpers;
using PopeyeClub.Hubs;
using PopeyeClub.Services.Dto.Chat;
using PopeyeClub.Services.Interfaces;
using PopeyeClub.ViewModels.Chat;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace PopeyeClub.Controllers
{
    [Authorize]
    public class ChatController : Controller
    {
        private readonly IChatService chatService;
        private readonly IHubContext<ChatHub> hub;
        private readonly IMessageService messageService;
        private readonly IUserService userService;

        public ChatController(IChatService chatService, IHubContext<ChatHub> hub, IMessageService messageService, IUserService userService)
        {
            this.chatService = chatService;
            this.hub = hub;
            this.messageService = messageService;
            this.userService = userService;
        }

        public IActionResult Overview()
        {
            string currentUserId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            List<ChatRoomViewModel> models = chatService.GetAllRooms(currentUserId).Select(x => x.ToChatRoomViewModel()).ToList();
            return View(models);
        }

        public IActionResult CreateRoom(string userId)
        {
            if(userId != default)
            {
                chatService.Create(userId, User.FindFirst(ClaimTypes.NameIdentifier).Value);
            }
            return RedirectToAction(nameof(Overview));
        }

        [HttpPost]
        public async Task<IActionResult> JoinRoom(string connectionId, string chatroomName)
        {
            string currentUserId = User.FindFirst(ClaimTypes.NameIdentifier).Value;

            JoinRoomDto room = await chatService.GetByRoomName(chatroomName, currentUserId);

            if(room != null)
            {
                await hub.Groups.AddToGroupAsync(connectionId, chatroomName);
                return Ok(room);
            }
            else
            {
                return BadRequest(room);
            }
        }

        [HttpPost]
        public async Task<IActionResult> SendMessage(string text, string chatroomName, int chatroomId)
        {
            if (text == default || chatroomId == default)
            {
                return BadRequest();
            }
            else
            {
                string userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
                MessageDto message = await messageService.Create(userId, chatroomId, text);
                await hub.Clients.Group(chatroomName).SendAsync("RecieveMessage", message);
                return Ok();
            }
        }
    }
}