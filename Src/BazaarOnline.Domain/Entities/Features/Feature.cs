using BazaarOnline.Domain.Entities.Categories;

namespace BazaarOnline.Domain.Entities.Features;

public class Feature
{
    public int Id { get; set; }

    public string Name { get; set; }

    public string Description { get; set; }

    public FeaturePositionEnum Position { get; set; }

    #region Type

    public FeatureTypeEnum Type { get; set; }

    public object TypeObject
    {
        get
        {
            switch (Type)
            {
                case FeatureTypeEnum.Integer:
                    return IntegerType;

                case FeatureTypeEnum.Select:
                    return SelectType;

                case FeatureTypeEnum.String:
                    return StringType;

                default:
                    return null;
            }
        }
    }

    public int? StringTypeId { get; set; }

    public int? IntegerTypeId { get; set; }

    public int? SelectTypeId { get; set; }

    public FeatureStringType? StringType { get; set; }

    public FeatureIntegerType? IntegerType { get; set; }

    public FeatureSelectType? SelectType { get; set; }

    #endregion

    #region Relations

    public IEnumerable<CategoryFeature> CategoryFeatures { get; set; }

    #endregion
}