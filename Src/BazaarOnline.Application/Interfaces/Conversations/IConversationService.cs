using BazaarOnline.Application.DTOs;
using BazaarOnline.Application.DTOs.ConversationDTOs;
using BazaarOnline.Application.ViewModels.Conversations;

namespace BazaarOnline.Application.Interfaces.Conversations;

public interface IConversationService
{
    AddConversationResultDTO AddConversation(AddConversationDTO dto, string userId);
    AddMessageResultDTO AddMessage(AddMessageDTO dto, string userId);

    IEnumerable<ConversationDetailViewModel> GetConversations(string userId);

    IEnumerable<MessageDetailViewModel> GetConversationMessages(Guid conversationId, string userId);

    bool IsConversationExists(Guid conversationId, string userId);

    MessageDetailViewModel GetMessage(Guid messageId, string userId);

    string? GetSecondConversationUser(Guid conversationId, string userId);

    OperationResultDTO SeenConversation(Guid conversationId, string userId);
    OperationResultDTO SeenMessage(Guid conversationId, Guid messageId, string userId);
}