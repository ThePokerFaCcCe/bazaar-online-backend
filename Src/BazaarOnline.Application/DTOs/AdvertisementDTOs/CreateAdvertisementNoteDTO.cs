using System.ComponentModel.DataAnnotations;

namespace BazaarOnline.Application.DTOs.AdvertisementDTOs;

public class CreateAdvertisementNoteDTO
{
    [Display(Name = "یادداشت")]
    [Required(ErrorMessage = "این فیلد اجباری است")]
    [MaxLength(255, ErrorMessage = "{0} باید حداکثر {1} کاراکتر باشد")]
    public string Note { get; set; }
}