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

        public DateTime CreateDate { get; set; }

        public Guid? ReplyToId { get; set; }

        public string SenderId { get; set; }

        public Guid ConversationId { get; set; }


        #region Relations

        public Message ReplyTo { get; set; }
        public IEnumerable<Message> Replies { get; set; }
        public User Sender { get; set; }
        public Conversation Conversation { get; set; }

        #endregion
    }
}