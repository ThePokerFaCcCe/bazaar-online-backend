using System.ComponentModel.DataAnnotations;
using BazaarOnline.Application.Utils.Extensions;

namespace BazaarOnline.Application.DTOs.ConversationDTOs;

public class ChattingStatusDTO
{
    public Guid ConversationId { get; set; }
    public ChattingStatusEnum Status { get; set; }
    public int Timeout { get; set; } = 3;
    public string StatusName => Status.GetDisplayName();
}

public enum ChattingStatusEnum
{
    [Display(Name = "Nothing")] Nothing = 0,
    [Display(Name = "Typing")] Typing = 1,
    [Display(Name = "RecordingVoice")] RecordingVoice = 2,
}