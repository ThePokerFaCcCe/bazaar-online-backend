namespace BazaarOnline.Application.Interfaces.Hubs;

public interface IChatHub
{
    Task ReceiveMessage(string jsonData);
}