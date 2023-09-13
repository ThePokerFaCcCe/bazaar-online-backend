namespace BazaarOnline.Application.DTOs.ConversationDTOs;

public class MessageOperationResultDTO
{
    public Guid? MessageId { get; set; }

    public Dictionary<string,object> ErrorMessage { get; set; } = new Dictionary<string, object>();

    public int ErrorCode { get; set; } = 0;

    public bool IsSuccess => MessageId != null;
}