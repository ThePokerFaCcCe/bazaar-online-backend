using BazaarOnline.Application.DTOs.ConversationDTOs;

namespace BazaarOnline.Application.Interfaces.Conversations;

public interface IConversationService
{
    AddConversationResultDTO AddConversation(AddConversationDTO dto, string userId);
    AddMessageResultDTO AddMessage(AddMessageDTO dto, string userId);
}