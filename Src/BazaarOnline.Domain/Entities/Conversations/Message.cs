using BazaarOnline.Domain.Entities.Users;

namespace BazaarOnline.Domain.Entities.Conversations
{
    public class Message
    {
        public Guid Id { get; set; }

        public string Text { get; set; }

        public string? AttachmentJson { get; set; }

        public MessageAttachmentTypeEnum AttachmentType { get; set; }

        public bool IsDeleted { get; set; }

        public bool IsSeen { get; set; }

        public bool IsEdited { get; set; }

        public DateTime CreateDate { get; set; }

        public DateTime? UpdateDate { get; set; }

        public Guid? ReplyToId { get; set; }

        public string SenderId { get; set; }

        public Guid ConversationId { get; set; }


        #region Relations

        public Message? ReplyTo { get; set; }
        public IEnumerable<Message> Replies { get; set; }
        public IEnumerable<DeletedMessage> DeletedMessages { get; set; }
        public User Sender { get; set; }
        public Conversation Conversation { get; set; }

        #endregion
    }
}