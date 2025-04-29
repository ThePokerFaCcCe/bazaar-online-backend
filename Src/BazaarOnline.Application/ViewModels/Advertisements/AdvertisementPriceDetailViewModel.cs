using BazaarOnline.Domain.Entities.Advertisements;

namespace BazaarOnline.Application.ViewModels.Advertisements;

public class AdvertisementPriceDetailViewModel
{
    public AdvertisementPriceTypeEnum Type { get; set; } = AdvertisementPriceTypeEnum.Agreement;
    public long Value { get; set; } = 0;
}