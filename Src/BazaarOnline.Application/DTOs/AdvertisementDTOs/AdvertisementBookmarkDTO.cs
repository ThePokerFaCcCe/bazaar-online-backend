using System.ComponentModel.DataAnnotations;

namespace BazaarOnline.Application.DTOs.AdvertisementDTOs;

public class AdvertisementBookmarkDTO
{
    [Required(ErrorMessage = "این فیلد اجباری است")]
    public bool IsBookmarked { get; set; }
}