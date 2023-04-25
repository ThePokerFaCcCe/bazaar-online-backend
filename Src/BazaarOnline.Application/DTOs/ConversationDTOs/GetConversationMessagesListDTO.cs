using System.ComponentModel.DataAnnotations;

namespace BazaarOnline.Application.DTOs.ConversationDTOs
{
    public class GetConversationMessagesListDTO
    {
        [Required(ErrorMessage = "این فیلد اجباری است")]
        public Guid ConversatinonId { get; set; }
    }
}
