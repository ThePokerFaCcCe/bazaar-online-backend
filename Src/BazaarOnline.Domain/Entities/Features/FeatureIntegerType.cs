namespace BazaarOnline.Domain.Entities.Features;

public class FeatureIntegerType
{
    public int Id { get; set; }

    public long Minimum { get; set; }

    public long Maximum { get; set; }

    public string Placeholder { get; set; }

    #region Relations

    public IEnumerable<Feature> Features { get; set; }

    #endregion
}