using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using BazaarOnline.Application.Filters.Generic.Attributes;
using BazaarOnline.Domain.Entities.Maps;

namespace BazaarOnline.Application.DTOs.MapDTOs
{
    public class ProvinceFilterDTO
    {
        [Filter(FilterTypeEnum.ModelContainsThis, ModelPropertyName = nameof(Province.Name))]
        [DisplayName("نام استان")]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "{0} باید بین {1} و {2} کاراکتر باشد")]
        public string? Name { get; set; }
    }
}