using BazaarOnline.Application.DTOs;
using BazaarOnline.Application.DTOs.ConversationDTOs;
using BazaarOnline.Application.DTOs.PaginationDTO;
using BazaarOnline.Application.ViewModels.Conversations;

namespace BazaarOnline.Application.Interfaces.Conversations;

public interface IConversationService
{
    AddConversationResultDTO AddConversation(AddConversationDTO dto, string userId);
    void DeleteConversation(Guid conversationId, string userId);

    MessageOperationResultDTO AddMessage(AddMessageDTO dto, string userId);

    MessageOperationResultDTO EditMessage(EditMessageDTO dto, string userId);

    MessageOperationResultDTO DeleteMessage(DeleteMessageDTO dto, string userId);

    IEnumerable<ConversationDetailViewModel> GetConversations(string userId);

    PaginationResultDTO<MessageDetailViewModel> GetConversationMessages(Guid conversationId, string userId,
        PaginationFilterDTO pagination);

    bool IsConversationExists(Guid conversationId, string userId);

    MessageDetailViewModel GetMessage(Guid messageId, string userId);

    string? GetSecondConversationUser(Guid conversationId, string userId);
    IEnumerable<ConversationUserIdViewModel> GetUserIdsHaveConversationWithUser(string userId);

    OperationResultDTO SeenConversation(Guid conversationId,string userId);
    OperationResultDTO SeenMessage(Guid conversationId, Guid messageId, string userId);
    OperationResultDTO BlockUser(BlockUserDTO dto, string blockerUserId);
    OperationResultDTO UnblockUser(UnblockUserDTO dto, string blockerUserId);
    OperationResultDTO BulkDeleteConversations(IEnumerable<Guid> conversationIds, string userId);

    IEnumerable<BlocklistViewModel> GetUserBlocklist(string userId);
}