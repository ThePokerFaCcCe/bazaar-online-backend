using System.ComponentModel.DataAnnotations;

namespace BazaarOnline.Application.DTOs.ConversationDTOs;

public enum SocketEventTypeEnum
{
    [Display(Name = "SeenConversation")] SeenConversation = 1,
    [Display(Name = "Chatting")] Chatting = 2,
    [Display(Name = "SeenMessage")] SeenMessage = 3,
    [Display(Name = "EditMessage")] EditMessage = 4,
    [Display(Name = "DeleteMessage")] DeleteMessage = 5,
    [Display(Name = "UserOnline")] UserOnline = 6,
    [Display(Name = "NewConversation")] NewConversation = 7,
    [Display(Name = "UserOffline")] UserOffline = 8,
}