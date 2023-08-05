namespace BazaarOnline.Application.DTOs.ConversationDTOs
{
    public class AddConversationResultDTO
    {
        public Guid? ConversationId { get; set; }
        public string ReceiverUserId { get; set; }  = string.Empty;
        public string ErrorMessage { get; set; } = string.Empty;

        public int ErrorCode { get; set; } = 0;

        public bool IsSuccess => ConversationId != null;
    }
}