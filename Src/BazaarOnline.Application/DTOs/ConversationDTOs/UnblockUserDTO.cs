using System.ComponentModel.DataAnnotations;

namespace BazaarOnline.Application.DTOs.ConversationDTOs;

public class UnblockUserDTO
{
    [Required(ErrorMessage = "این فیلد اجباری است")]
    public string UserId { get; set; }
}