namespace BazaarOnline.Application.ViewModels.Advertisements;

public class AdvertisementSearchSuggestViewModel
{
    public int CategoryId { get; set; } = 0;
    public string CategoryTitle { get; set; } = string.Empty;
    public string SearchSuggest { get; set; } = string.Empty;
    public int AdvertisementsCount { get; set; } = 0;
}