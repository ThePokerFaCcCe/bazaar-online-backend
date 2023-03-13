namespace BazaarOnline.Application.ViewModels.Advertisements;

public class AdvertisementListDetailViewModel
{
    public int Id { get; set; }

    public string Title { get; set; }

    public IEnumerable<AdvertisementFeatureDetailViewModel> Features { get; set; }

    public DateTime UpdateDate { get; set; }

    public AdvertisementPictureViewModel? Picture { get; set; }

    public string TimeText { get; set; }

    public string LocationText { get; set; }

    public string InfoText => $"{TimeText} در {LocationText}";
}