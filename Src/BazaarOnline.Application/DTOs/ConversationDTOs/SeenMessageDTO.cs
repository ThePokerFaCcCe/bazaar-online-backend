namespace BazaarOnline.Application.DTOs.ConversationDTOs;

public class SeenMessageDTO
{
    public Guid ConversationId { get; set; } = Guid.Empty;
    public Guid MessageId { get; set; } = Guid.Empty;

    /// <summary>
    /// Index of message that seen - used for frontend and not important in backend
    /// </summary>
    public int? MessageIndexInquiry { get; set; }
}