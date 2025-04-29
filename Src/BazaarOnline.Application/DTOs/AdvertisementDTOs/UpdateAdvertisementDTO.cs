using BazaarOnline.Application.Validators.Attributes;
using BazaarOnline.Domain.Entities.Advertisements;
using System.ComponentModel.DataAnnotations;

namespace BazaarOnline.Application.DTOs.AdvertisementDTOs;

public class UpdateAdvertisementDTO
{
    [Display(Name = "موضوع")]
    [Required(ErrorMessage = "این فیلد اجباری است")]
    [MaxLength(50, ErrorMessage = "{0} باید حداکثر {1} کاراکتر باشد")]
    public string Title { get; set; }

    [Display(Name = "توضیحات")]
    [Required(ErrorMessage = "این فیلد اجباری است")]
    [MaxLength(500, ErrorMessage = "{0} باید حداکثر {1} کاراکتر باشد")]
    public string Description { get; set; }

    [Display(Name = "آدرس")]
    [Required(ErrorMessage = "این فیلد اجباری است")]
    [MaxLength(100, ErrorMessage = "{0} باید حداکثر {1} کاراکتر باشد")]
    public string Address { get; set; }

    public double? Longitude { get; set; }

    public double? Latitude { get; set; }

    [Required(ErrorMessage = "این فیلد اجباری است")]
    public AdvertisementPriceInputDTO Price { get; set; }

    public AdvertisementPriceTypeEnum PriceType => Price.Type;
    public long PriceValue => Price.Value;

    public bool ShowExactCoordinates { get; set; }

    [Display(Name = "نوع تماس")]
    [Required(ErrorMessage = "این فیلد اجباری است")]
    public AdvertisementContactTypeEnum ContactType { get; set; }

    [ValidateImage]
    public List<int>? Pictures { get; set; }
        = new List<int>();

    public List<CreateAdvertisementFeatureDTO> Features { get; set; }
        = new List<CreateAdvertisementFeatureDTO>();
}