using BazaarOnline.Application.DTOs.ConversationDTOs;

namespace BazaarOnline.Application.Interfaces.Conversations;

public interface IConversationService
{
    AddConversationResultDTO AddConversation(AddConversationDTO dto, string userId);
    void AddMessage(AddMessageDTO dto, string userId);
}
