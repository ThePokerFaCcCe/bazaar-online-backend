using System.ComponentModel.DataAnnotations;

namespace BazaarOnline.Application.DTOs.ConversationDTOs;

public class DeleteConversationDTO
{
    [Required(ErrorMessage = "این فیلد اجباری است")]
    public Guid ConversationId { get; set; }
}