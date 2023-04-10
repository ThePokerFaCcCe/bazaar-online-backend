namespace BazaarOnline.Domain.Entities.Conversations
{
    public class Message
    {
        public int Id { get; set; }

        public string Content { get; set; }
        
        public MessageContentTypeEnum ContentType { get; set; }

        public int ReplyToId { get; set; }


        #region Relations

        public Message ReplyTo { get; set; }

        #endregion
    }
}
