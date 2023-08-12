using System.ComponentModel.DataAnnotations;

namespace BazaarOnline.Application.DTOs.ConversationDTOs;

public enum SocketOperationTypeEnum
{
    [Display(Name = "SendMessage")] SendMessage = 1,
    [Display(Name = "SeenConversation")] SeenConversation = 2,
    [Display(Name = "ChattingStatus")] ChattingStatus = 3,
    [Display(Name = "SeenMessage")] SeenMessage = 4,
    [Display(Name = "EditMessage")] EditMessage = 5,
    [Display(Name = "DeleteMessage")] DeleteMessage = 6,
    [Display(Name = "ImOnline")] ImOnline = 7,
    [Display(Name = "ImOffline")] ImOffline = 8,
}