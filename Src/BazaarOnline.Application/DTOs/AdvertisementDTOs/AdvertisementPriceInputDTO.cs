using System.ComponentModel.DataAnnotations;
using BazaarOnline.Domain.Entities.Advertisements;

namespace BazaarOnline.Application.DTOs.AdvertisementDTOs;

public class AdvertisementPriceInputDTO
{

    [Required(ErrorMessage = "این فیلد اجباری است")]
    public AdvertisementPriceTypeEnum Type { get; set; }
    public long Value { get; set; } = 0;
}