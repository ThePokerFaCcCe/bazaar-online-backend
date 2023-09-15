using BazaarOnline.Application.Filters.Generic.Attributes;
using BazaarOnline.Domain.Entities.Advertisements;
using BazaarOnline.Domain.Entities.Maps;

namespace BazaarOnline.Application.DTOs.MapDTOs;

public class CityFilterDTO
{
    [Filter(FilterTypeEnum.ThisContainsModel, ModelPropertyName = nameof(City.Id))]
    public List<int>? CityIds { get; set; }
}