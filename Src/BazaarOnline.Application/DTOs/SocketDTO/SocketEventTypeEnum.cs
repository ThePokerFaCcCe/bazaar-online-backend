using System.ComponentModel.DataAnnotations;

namespace BazaarOnline.Application.DTOs.ConversationDTOs;

public enum SocketEventTypeEnum
{
    [Display(Name = "Seen")] Seen = 1,
}