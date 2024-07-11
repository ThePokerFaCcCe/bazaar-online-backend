using BazaarOnline.Domain.Entities.Users;

namespace BazaarOnline.Domain.Entities.Conversations;

public class DeletedMessage
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public DateTime DeleteDate { get; set; }

    public string UserId { get; set; }

    public Guid ConversationId { get; set; }

    public Guid MessageId { get; set; }


    #region Relations

    public User User { get; set; }

    public Conversation Conversation { get; set; }

    public Message Message { get; set; }

    #endregion
}