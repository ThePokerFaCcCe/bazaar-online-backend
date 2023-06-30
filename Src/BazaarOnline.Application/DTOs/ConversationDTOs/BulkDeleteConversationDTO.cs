using System.ComponentModel.DataAnnotations;

namespace BazaarOnline.Application.DTOs.ConversationDTOs;

public class BulkDeleteConversationDTO
{
    [Required(ErrorMessage = "این فیلد اجباری است")]
    public IEnumerable<Guid> ConversationIds { get; set; }
}