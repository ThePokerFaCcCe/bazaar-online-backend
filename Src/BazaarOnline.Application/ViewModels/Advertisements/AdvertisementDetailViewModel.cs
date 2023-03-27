using BazaarOnline.Application.Utils;
using BazaarOnline.Domain.Entities.Advertisements;

namespace BazaarOnline.Application.ViewModels.Advertisements;

public class AdvertisementDetailViewModel
{
    public int Id { get; set; }

    public AdvertisementDetailDataViewModel Data { get; set; }
}

public class AdvertisementDetailDataViewModel
{
    public string Title { get; set; } = string.Empty;

    public string Description { get; set; } = string.Empty;

    public string Note { get; set; } = string.Empty;

    public bool IsBookmarked { get; set; } = false;

    public string Address { get; set; } = string.Empty;

    public AdvertisementProvinceDetailViewModel Province { get; set; } = new AdvertisementProvinceDetailViewModel();

    public AdvertisementCityDetailViewModel City { get; set; } = new AdvertisementCityDetailViewModel();

    public AdvertisementLocationDetailViewModel Location { get; set; } = new AdvertisementLocationDetailViewModel();

    public AdvertisementContactTypeEnum ContactType { get; set; } = AdvertisementContactTypeEnum.Normal;

    public bool IsChatEnabled =>
        (ContactType is AdvertisementContactTypeEnum.Normal or AdvertisementContactTypeEnum.ChatOnly);

    public DateTime CreateDate { get; set; } = DateTime.MinValue;

    public DateTime UpdateDate { get; set; } = DateTime.MinValue;

    public string TimeText => UpdateDate.PassedFromNowString();

    public IEnumerable<AdvertisementPictureViewModel> Pictures { get; set; } =
        Enumerable.Empty<AdvertisementPictureViewModel>();

    public IEnumerable<AdvertisementFeatureDetailViewModel> TopFeatures { get; set; } =
        Enumerable.Empty<AdvertisementFeatureDetailViewModel>();

    public IEnumerable<AdvertisementFeatureDetailViewModel> MiddleFeatures { get; set; } =
        Enumerable.Empty<AdvertisementFeatureDetailViewModel>();

    public IEnumerable<AdvertisementFeatureDetailViewModel> BottomFeatures { get; set; } =
        Enumerable.Empty<AdvertisementFeatureDetailViewModel>();

    public IEnumerable<AdvertisementFeatureDetailViewModel> OtherFeatures { get; set; } =
        Enumerable.Empty<AdvertisementFeatureDetailViewModel>();

    public IEnumerable<AdvertisementCategoryDetailViewModel> CategoryPath { get; set; } =
        Enumerable.Empty<AdvertisementCategoryDetailViewModel>();
}