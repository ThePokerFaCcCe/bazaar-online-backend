namespace BazaarOnline.Application.DTOs.ConversationDTOs;

public class SocketOperationResultDTO
{
    public string InquiryId { get; set; }
    public string ServerErrorMessage { get; set; } = string.Empty;
    public bool IsSuccess { get; set; }
    public object? Data { get; set; }
}