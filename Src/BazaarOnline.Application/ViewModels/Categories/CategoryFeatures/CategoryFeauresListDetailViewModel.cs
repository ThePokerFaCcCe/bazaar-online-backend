using BazaarOnline.Application.Generators;
using BazaarOnline.Application.Utils.Extensions;
using BazaarOnline.Domain.Entities.Features;

namespace BazaarOnline.Application.ViewModels.Categories.CategoryFeatures;

// Changed for front developer request.

public class CategoryFeaturesListDetailViewModel
{
    public int Id { get; set; }

    public CategoryFeaturesListFeatureDetailViewModel Data { get; set; }
}

public class CategoryFeaturesListFeatureDetailViewModel
{
    public bool IsRequired { get; set; }

    public string Name { get; set; }

    public string Description { get; set; }

    public FeatureTypeEnum Type { get; set; }

    public string TypeName => Type.GetDisplayName();

    //front developer request
    public string UniqueKey => $"{TypeName}-{StringGenerator.GenerateUniqueCodeWithoutDash()}";

    //front developer request
    public string Value { get; set; } = string.Empty;

    #region StringValidation

    public int MinLength { get; set; }

    public int MaxLength { get; set; }

    public string Regex { get; set; }

    #endregion

    #region IntegerValidation

    public long Minimum { get; set; }

    public long Maximum { get; set; }

    #endregion

    #region SelectValidation

    public string Options
    {
        set => OptionsList = value?.Split('\u002C' /* , */);
    }

    public IEnumerable<string> OptionsList { get; set; }

    #endregion

    public string Placeholder { get; set; }
}