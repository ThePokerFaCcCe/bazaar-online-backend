using BazaarOnline.Application.Filters.Generic.Attributes;
using BazaarOnline.Domain.Entities.Advertisements;

namespace BazaarOnline.Application.DTOs.AdvertisementDTOs;

public class AdvertisemenFilterDTO
{
    [Filter(FilterTypeEnum.ModelContainsThis)]
    public string? Title { get; set; }

    //[Filter(FilterTypeEnum.Equals, ModelPropertyName = nameof(Advertisement.CategoryId))]
    public int? Category { get; set; }

    [Filter(FilterTypeEnum.ThisContainsModel, ModelPropertyName = nameof(Advertisement.CityId))]
    public List<int>? Cities { get; set; }

    public long? StartPrice { get; set; }

    public long? EndPrice { get; set; }

    public bool HasPicture { get; set; } = false;

    [Order(nameof(Advertisement.Title))]
    [Order(nameof(Advertisement.UpdateDate))]
    public string? OrderBy { get; set; } = $"-{nameof(Advertisement.UpdateDate)}";
}