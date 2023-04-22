namespace BazaarOnline.Application.Interfaces.Hubs;

public interface IChatHub
{
    Task ReceiveMessage(string jsonData);
    Task ReceiveEvent(string jsonData);
    Task ReceiveOperationResult(string jsonData);
}