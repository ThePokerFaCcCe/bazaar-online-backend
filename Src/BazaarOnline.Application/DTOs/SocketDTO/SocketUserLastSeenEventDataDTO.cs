namespace BazaarOnline.Application.DTOs.ConversationDTOs;

public class SocketUserLastSeenEventDataDTO
{
    public DateTime LastSeen { get; set; }

    public string UserId { get; set; }

    public Guid ConversationId { get; set; }
}