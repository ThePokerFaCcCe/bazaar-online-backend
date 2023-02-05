using BazaarOnline.Application.Validators.Attributes;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;
using BazaarOnline.Domain.Entities.Advertisements;

namespace BazaarOnline.Application.DTOs.AdvertisementDTOs;

public class CreateAdvertisementDTO
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

    [Required(ErrorMessage = "این فیلد اجباری است")]
    public double Longitude { get; set; }

    [Required(ErrorMessage = "این فیلد اجباری است")]
    public double Latitude { get; set; }

    [Display(Name = "نوع تماس")]
    [Required(ErrorMessage = "این فیلد اجباری است")]
    public AdvertisementContactTypeEnum ContactType { get; set; }

    [Display(Name = "دسته بندی")]
    [Required(ErrorMessage = "این فیلد اجباری است")]
    public int CategoryId { get; set; }

    [Display(Name = "استان")]
    [Required(ErrorMessage = "این فیلد اجباری است")]
    public int ProvinceId { get; set; }

    [Display(Name = "شهر")]
    [Required(ErrorMessage = "این فیلد اجباری است")]
    public int CityId { get; set; }

    [ValidateImage]
    public List<IFormFile>? Pictures { get; set; }
        = new List<IFormFile>();

    public List<CreateAdvertisementFeatureDTO> Features { get; set; }
        = new List<CreateAdvertisementFeatureDTO>();
}