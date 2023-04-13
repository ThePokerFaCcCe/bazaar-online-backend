using System.ComponentModel.DataAnnotations;

namespace BazaarOnline.Application.DTOs.ConversationDTOs
{
    public class AddConversationDTO
    {
        [Required(ErrorMessage = "این فیلد اجباری است")]
        public string UserId { get; set; }

        [Required(ErrorMessage = "این فیلد اجباری است")]
        public int AdvertisementId { get; set; }
    }
}
