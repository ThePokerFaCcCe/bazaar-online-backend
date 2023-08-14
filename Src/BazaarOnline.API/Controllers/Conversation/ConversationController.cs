using BazaarOnline.API.Hubs;
using BazaarOnline.Application.DTOs.ConversationDTOs;
using BazaarOnline.Application.DTOs.PaginationDTO;
using BazaarOnline.Application.Interfaces.Conversations;
using BazaarOnline.Application.Utils.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

namespace BazaarOnline.API.Controllers.Conversations
{
    [Route("api/v1/conversations")]
    [ApiController]
    [Authorize]
    public class ConversationController : ControllerBase
    {
        private readonly IConversationService _conversationService;
        private readonly IHubContext<ChatHub> _chatHubContext;
        public ConversationController(IConversationService conversationService, IHubContext<ChatHub> chatHubContext)
        {
            _conversationService = conversationService;
            _chatHubContext = chatHubContext;
        }

        [HttpGet("")]
        public IActionResult GetConversationsList()
        {
            var userId = User.Identity.Name;
            var conversations = _conversationService.GetConversations(userId);
            return Ok(conversations);
        }

        [HttpPost("")]
        public async Task<IActionResult> CreateConversation([FromBody] AddConversationDTO dto)
        {
            if (!ModelState.IsValid) return BadRequest();

            var userId = User.Identity.Name;

            var conversationResult = _conversationService.AddConversation(dto, userId);
            if (!conversationResult.IsSuccess) return StatusCode(400, conversationResult);

            
            return Ok(conversationResult);
        }

        [HttpPost("{id:guid}/block")]
        public IActionResult BlockUser(Guid id)
        {
            var userId = User.Identity.Name;
            var secondUser = _conversationService.GetSecondConversationUser(id, userId);
            if (string.IsNullOrEmpty(secondUser))
            {
                return NotFound();
            }

            var conversationResult = _conversationService.BlockUser(new BlockUserDTO { UserId = secondUser }, userId);
            return Ok(conversationResult);
            //return conversationResult.IsSuccess ? Ok(conversationResult) : BadRequest(conversationResult);
        }


        [HttpGet("blocklist")]
        public IActionResult BlocklistUser()
        {
            var userId = User.Identity.Name;

            var conversationResult = _conversationService.GetUserBlocklist(userId);
            return Ok(conversationResult);
        }


        [HttpPost("unblock")]
        public IActionResult UnblockUser([FromBody] UnblockUserRequestDTO dto)
        {
            if (!ModelState.IsValid) return BadRequest();

            var userId = User.Identity.Name;
            string blockUser = string.Empty;

            switch (dto.UnblockType)
            {
                case UnblockUserRequestTypeEnum.ByConversationId:
                    blockUser = _conversationService.GetSecondConversationUser(dto.Id, userId);
                    break;
                case UnblockUserRequestTypeEnum.ByUserId:
                    blockUser = dto.Id.ToString();
                    break;
            }

            if (string.IsNullOrEmpty(blockUser))
            {
                return NotFound();
            }

            var conversationResult =
                _conversationService.UnblockUser(new UnblockUserDTO { UserId = blockUser }, userId);
            if (!conversationResult.IsSuccess)
            {
                return BadRequest(conversationResult);
            }
            return Ok(conversationResult);
            //return conversationResult.IsSuccess ? Ok(conversationResult) : BadRequest(conversationResult);
        }



        [HttpDelete("bulk")]
        public IActionResult BulkDeleteConversation(BulkDeleteConversationDTO dto)
        {
            if (!ModelState.IsValid) return BadRequest();

            var userId = User.Identity.Name;
            var result = _conversationService.BulkDeleteConversations(dto.ConversationIds, userId);
            if (!result.IsSuccess)
            {
                ModelState.AddModelError(nameof(dto.ConversationIds), result.Message);
                return ValidationProblem(ModelState);
            }

            return NoContent();
        }

        [HttpDelete("{id:guid}")]
        public IActionResult DeleteConversation(Guid id)
        {
            var userId = User.Identity.Name;
            _conversationService.DeleteConversation(id, userId);
            return NoContent();
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