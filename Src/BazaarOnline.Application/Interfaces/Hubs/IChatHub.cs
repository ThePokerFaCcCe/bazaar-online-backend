namespace BazaarOnline.Application.Interfaces.Hubs;

public interface IChatHub
{
    Task ReceiveMessage(string inquiryId, string jsonData);
    Task ReceiveEvent(string jsonData);
    Task ReceiveOperationResult(string jsonData);
}