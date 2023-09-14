namespace BazaarOnline.Application.DTOs.ConversationDTOs;

public class SeenMessageDTO
{
    public Guid ConversationId { get; set; } = Guid.Empty;
    public Guid MessageId { get; set; } = Guid.Empty;
}