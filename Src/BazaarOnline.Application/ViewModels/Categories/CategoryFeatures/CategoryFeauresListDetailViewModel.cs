using BazaarOnline.Domain.Entities.Features;

namespace BazaarOnline.Application.ViewModels.Categories.CategoryFeatures;

public class CategoryFeaturesListDetailViewModel
{
    public int Id { get; set; }

    public bool IsRequired { get; set; }

    public CategoryFeaturesListFeatureDetailViewModel Feature { get; set; }
}

public class CategoryFeaturesListFeatureDetailViewModel
{
    public string Name { get; set; }

    public string Description { get; set; }

    public FeatureTypeEnum Type { get; set; }

    public object TypeObject { get; set; }
}