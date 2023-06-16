using System.ComponentModel.DataAnnotations;

namespace BazaarOnline.Application.DTOs.ConversationDTOs;

public class DeleteMessageDTO
{
    [Required(ErrorMessage = "این فیلد اجباری است")]
    public Guid ConversationId { get; set; }

    [Required(ErrorMessage = "این فیلد اجباری است")]
    public Guid MessageId { get; set; }
}