using BazaarOnline.Domain.Entities.Features;

namespace BazaarOnline.Application.ViewModels.Advertisements;

public class AdvertisementFeatureDetailViewModel
{
    public int Id { get; set; }

    public string Value { get; set; }

    public int SortNumber { get; set; }

    public string Name { get; set; }

    public FeaturePositionEnum Position { get; set; }
}