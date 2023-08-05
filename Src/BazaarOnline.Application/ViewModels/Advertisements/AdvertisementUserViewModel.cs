namespace BazaarOnline.Application.ViewModels.Advertisements;

public class AdvertisementUserViewModel
{
    public string Id { get; set; } = string.Empty;
    public string DisplayName { get; set; }  = string.Empty;
    public bool IsSelfAdvertisement { get; set; }
}