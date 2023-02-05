using System.ComponentModel.DataAnnotations;

namespace BazaarOnline.Application.DTOs.AdvertisementDTOs;

public class CreateAdvertisementFeatureDTO
{
    /// <summary>
    /// CategoryFeatureId
    /// </summary>
    [Required(ErrorMessage = "این فیلد اجباری است")]
    public int Id { get; set; }

    [Required(ErrorMessage = "این فیلد اجباری است")]
    public string Value { get; set; }
}