using BazaarOnline.Domain.Entities.Categories;

namespace BazaarOnline.Domain.Entities.Advertisements;

public class AdvertisementFeature
{
    public int Id { get; set; }

    public string Value { get; set; }

    public int AdvertisementId { get; set; }

    public int CategoryFeatureId { get; set; }

    #region Relations

    public Advertisement Advertisement { get; set; }
    public CategoryFeature CategoryFeature { get; set; }

    #endregion
}