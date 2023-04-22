namespace BazaarOnline.Application.DTOs.ConversationDTOs;

public class SocketOperaionRequestDTO<T>
{
    public string InquiryId { get; set; }
    public T Data { get; set; }
}