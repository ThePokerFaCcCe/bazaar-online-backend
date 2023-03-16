namespace BazaarOnline.Application.ViewModels.Advertisements;

public class AdvertisementListDetailViewModel
{
    public int Id { get; set; }
    public AdvertisementListDetailDataViewModel Data { get; set; }
}

public class AdvertisementListDetailDataViewModel
{
    public string Title { get; set; }

    public IEnumerable<AdvertisementFeatureDetailViewModel> Features { get; set; }

    public DateTime UpdateDate { get; set; }

    public bool IsChatEnabled { get; set; }

    public AdvertisementPictureViewModel? Picture { get; set; }

    public string TimeText { get; set; }

    public string LocationText { get; set; }

    public string InfoText => $"{TimeText} در {LocationText}";
}