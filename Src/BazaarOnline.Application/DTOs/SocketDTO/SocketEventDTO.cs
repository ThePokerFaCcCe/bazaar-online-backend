using BazaarOnline.Application.Utils.Extensions;

namespace BazaarOnline.Application.DTOs.ConversationDTOs;

public class SocketEventDTO
{
    public SocketEventTypeEnum EventType { get; set; }
    public string EventTypeName => EventType.GetDisplayName();
    public object Data { get; set; }
}