using BazaarOnline.Application.Utils.Extensions;

namespace BazaarOnline.Application.DTOs.ConversationDTOs;

public class SocketOperationResultDTO
{
    public string InquiryId { get; set; }
    public string ServerErrorMessage { get; set; } = string.Empty;
    public bool IsSuccess { get; set; }
    public SocketOperationTypeEnum OperationType { get; set; }
    public string OperationTypeName => OperationType.GetDisplayName();
    public object? Data { get; set; }
}