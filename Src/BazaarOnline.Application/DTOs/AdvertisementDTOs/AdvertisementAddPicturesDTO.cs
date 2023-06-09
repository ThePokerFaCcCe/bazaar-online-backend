using System.ComponentModel.DataAnnotations;

namespace BazaarOnline.Application.DTOs.AdvertisementDTOs;

public class AdvertisementAddPicturesDTO
{
    [Required] public List<int> Pictures { get; set; }
}