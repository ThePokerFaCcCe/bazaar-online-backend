using System.ComponentModel.DataAnnotations;

namespace BazaarOnline.Application.DTOs.ConversationDTOs
{
    public class MessageLocationAttachmentDTO
    {
        [Required(ErrorMessage = "این فیلد اجباری است")]
        public double Longitude { get; set; }

        [Required(ErrorMessage = "این فیلد اجباری است")]
        public double Latitude { get; set; }
    }
}
