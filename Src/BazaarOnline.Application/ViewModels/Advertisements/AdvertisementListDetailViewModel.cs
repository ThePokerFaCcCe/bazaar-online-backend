namespace BazaarOnline.Application.ViewModels.Advertisements;

public class AdvertisementListDetailViewModel
{
    public int Id { get; set; }
    public AdvertisementListDetailDataViewModel Data { get; set; }
}

public class AdvertisementListDetailDataViewModel
{
    public string Title { get; set; } = string.Empty;

    public IEnumerable<AdvertisementFeatureDetailViewModel> Features { get; set; } =
        Enumerable.Empty<AdvertisementFeatureDetailViewModel>();

    public DateTime UpdateDate { get; set; }

    public bool IsChatEnabled { get; set; }

    public AdvertisementPictureViewModel? Picture { get; set; }

    public string TimeText { get; set; } = string.Empty;

    public string LocationText { get; set; } = string.Empty;

    public string InfoText => $"{TimeText} در {LocationText}";
}