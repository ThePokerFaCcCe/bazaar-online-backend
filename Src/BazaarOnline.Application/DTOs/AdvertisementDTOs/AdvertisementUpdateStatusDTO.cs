using System.ComponentModel.DataAnnotations;
using BazaarOnline.Domain.Entities.Advertisements;

namespace BazaarOnline.Application.DTOs.AdvertisementDTOs
{
    public class AdvertisementUpdateStatusDTO
    {
        [Required(ErrorMessage = "این فیلد اجباری است")]
        public AdvertisementStatusTypeEnum StatusType { get; set; }

        [Display(Name = "توضیحات")]
        [MaxLength(500, ErrorMessage = "{0} باید حداکثر {1} کاراکتر باشد")]
        public string? StatusReason { get; set; }
    }
}