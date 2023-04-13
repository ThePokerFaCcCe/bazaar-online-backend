namespace BazaarOnline.Application.DTOs.ConversationDTOs
{
    public class AddConversationResultDTO
    {
        public Guid? ConversationId { get; set; }
        public string? Message { get; set; }

        public bool IsSuccess => ConversationId != null;

    }
}
