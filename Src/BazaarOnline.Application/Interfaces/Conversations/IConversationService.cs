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

    Dictionary<Guid, ConversationDetailViewModel> GetConversations(string userId);
    ConversationDetailViewModel? GetConversationDetail(Guid conversationId, string userId);

    PaginationResultGenericTypeDTO<Dictionary<Guid, MessageDetailViewModel>> GetConversationMessages(Guid conversationId, string userId,
        PaginationFilterDTO pagination);

    bool IsConversationExists(Guid conversationId, string userId);
    bool HasConversationAnyMessages(Guid conversationId, string userId);
    bool IsUserDeletedConversation(Guid conversationId, string userId);


    MessageDetailViewModel GetMessage(Guid messageId, string userId);
    IEnumerable<MessageDetailViewModel> GetMessageReplies(Guid messageId, string userId);

    /// <summary>
    /// Return second userid
    /// </summary>
    /// <param name="conversationId"></param>
    /// <param name="userId"></param>
    /// <param name="checkIsDeletedConversation">returns null if second user deleted conversation</param>
    /// <returns></returns>
    string? GetSecondConversationUser(Guid conversationId, string userId, bool checkIsDeletedConversation = false);
    IEnumerable<ConversationUserIdViewModel> GetUserIdsHaveConversationWithUser(string userId);

    OperationResultDTO SeenConversation(Guid conversationId, string userId);
    OperationResultDTO SeenMessage(Guid conversationId, Guid messageId, string userId);
    OperationResultDTO BlockUser(BlockUserDTO dto, string blockerUserId);
    OperationResultDTO UnblockUser(UnblockUserDTO dto, string blockerUserId);
    OperationResultDTO BulkDeleteConversations(IEnumerable<Guid> conversationIds, string userId);

    IEnumerable<BlocklistViewModel> GetUserBlocklist(string userId);
}