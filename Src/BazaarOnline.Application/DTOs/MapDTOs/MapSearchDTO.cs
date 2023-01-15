using BazaarOnline.Application.Filters.Generic.Attributes;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace BazaarOnline.Application.DTOs.MapDTOs
{
    public class MapSearchDTO
    {
        [Filter(FilterTypeEnum.ModelContainsThis)]
        [DisplayName("نام شهر یا استان")]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "{0} باید بین {1} و {2} کاراکتر باشد")]
        public string? Name { get; set; }
    }
}