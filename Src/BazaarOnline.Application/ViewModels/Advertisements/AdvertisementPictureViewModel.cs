using BazaarOnline.Application.Utils;

namespace BazaarOnline.Application.ViewModels.Advertisements;

public class AdvertisementPictureViewModel
{
    public int Id { get; set; }

    private string fileName = string.Empty;

    public string FileName
    {
        get => fileName;
        set => fileName = string.IsNullOrWhiteSpace(value) ? "" : value;
    }

    public string ImageUrl => $"/{Path.Combine(PathHelper.AdvertisementImages, FileName)}";
    public string ThumbUrl => $"/{Path.Combine(PathHelper.AdvertisementThumbs, FileName)}";
}