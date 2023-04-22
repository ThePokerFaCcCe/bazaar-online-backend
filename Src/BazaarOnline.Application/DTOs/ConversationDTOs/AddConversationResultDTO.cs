namespace BazaarOnline.Application.DTOs.ConversationDTOs
{
    public class AddConversationResultDTO
    {
        public Guid? ConversationId { get; set; }

        public string? ErrorMessage { get; set; }

        public int? ErrorCode { get; set; }

        public bool IsSuccess => ConversationId != null;
    }
}