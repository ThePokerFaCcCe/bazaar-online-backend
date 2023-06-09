using System.ComponentModel.DataAnnotations;

namespace BazaarOnline.Application.DTOs.AdvertisementDTOs;

public class AdvertisementDeletePicturesDTO
{
    [Required] public List<int> Pictures { get; set; }
}