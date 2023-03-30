using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace BazaarOnline.Application.DTOs.AdvertisementDTOs;

public class AdvertisementSearchDTO
{
    [DisplayName("متن")]
    [Required(ErrorMessage = "این فیلد اجباری است")]
    [StringLength(50, MinimumLength = 3, ErrorMessage = "{0} باید بین {1} و {2} کاراکتر باشد")]
    public string Query { get; set; } = string.Empty;
}