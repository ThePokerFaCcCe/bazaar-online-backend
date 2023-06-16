namespace BazaarOnline.Application.DTOs.ConversationDTOs;

public class MessageOperationResultDTO
{
    public Guid? MessageId { get; set; }

    public string ErrorMessage { get; set; } = string.Empty;

    public int ErrorCode { get; set; } = 0;

    public bool IsSuccess => MessageId != null;
}