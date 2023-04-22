namespace BazaarOnline.Application.DTOs.ConversationDTOs;

public class AddMessageResultDTO
{
    public Guid? MessageId { get; set; }

    public string? ErrorMessage { get; set; }

    public int? ErrorCode { get; set; }

    public bool IsSuccess => MessageId != null;
}