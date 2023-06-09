using BazaarOnline.Application.Utils;

namespace BazaarOnline.Application.ViewModels.Advertisements;

public class AdvertisementPictureViewModel
{
    public int Id { get; set; }
    public string FileName { get; set; }
    public string ImageUrl => $"/{Path.Combine(PathHelper.AdvertisementImages, FileName)}";
    public string ThumbUrl => $"/{Path.Combine(PathHelper.AdvertisementThumbs, FileName)}";
}