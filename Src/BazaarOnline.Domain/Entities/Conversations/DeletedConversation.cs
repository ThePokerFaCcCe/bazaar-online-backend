using BazaarOnline.Domain.Entities.Users;

namespace BazaarOnline.Domain.Entities.Conversations;

public class DeletedConversation
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string UserId { get; set; }
    public Guid ConversationId { get; set; }
    public DateTime DeleteDate { get; set; }

    #region Relations

    public User User { get; set; }
    public Conversation Conversation { get; set; }

    #endregion
}