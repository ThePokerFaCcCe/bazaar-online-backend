namespace BazaarOnline.Domain.Entities.Features;

public class FeatureStringType
{
    public int Id { get; set; }

    public int MinLength { get; set; }

    public int MaxLength { get; set; }

    public string? Regex { get; set; }

    public string Placeholder { get; set; }

    #region Relations

    public IEnumerable<Feature> Features { get; set; }

    #endregion
}