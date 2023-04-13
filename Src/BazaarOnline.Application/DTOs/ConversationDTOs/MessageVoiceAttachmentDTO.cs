using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace BazaarOnline.Application.DTOs.ConversationDTOs
{
    public class MessageVoiceAttachmentDTO
    {
        [Required(ErrorMessage = "این فیلد اجباری است")]
        public IFormFile File { get; set; }
    }
}
