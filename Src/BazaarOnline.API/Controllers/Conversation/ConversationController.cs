using BazaarOnline.Application.DTOs.PaginationDTO;
using BazaarOnline.Application.Interfaces.Conversations;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BazaarOnline.API.Controllers.Conversations
{
    [Route("api/v1/conversations")]
    [ApiController]
    [Authorize]
    public class ConversationController : ControllerBase
    {
        private readonly IConversationService _conversationService;

        public ConversationController(IConversationService conversationService)
        {
            _conversationService = conversationService;
        }

        [HttpGet("")]
        public IActionResult GetConversationsList([FromQuery] PaginationFilterDTO pagination)
        {
            var userId = User.Identity.Name;
            var conversations = _conversationService.GetConversations(userId, pagination);
            return Ok(conversations);
        }

        [HttpGet("{id:guid}/messages")]
        public IActionResult GetConversationMessagesList(Guid id, [FromQuery] PaginationFilterDTO pagination)
        {
            var userId = User.Identity.Name;
            if (!_conversationService.IsConversationExists(id, userId))
            {
                return NotFound();
            }

            var messages = _conversationService.GetConversationMessages(id, userId, pagination);
            return Ok(messages);
        }
    }
}