using BazaarOnline.Domain.Entities.Advertisements;
using BazaarOnline.Domain.Entities.Features;

namespace BazaarOnline.Domain.Entities.Categories;

public class CategoryFeature
{
    public int Id { get; set; }

    public int SortNumber { get; set; }

    public bool IsRequired { get; set; }

    public bool IsFilterable { get; set; }

    public bool IsShownInList { get; set; }

    public int CategoryId { get; set; }

    public int FeatureId { get; set; }

    #region Relations

    public Feature Feature { get; set; }
    public Category Category { get; set; }
    public IEnumerable<AdvertisementFeature> AdvertisementFeatures { get; set; }

    #endregion
}