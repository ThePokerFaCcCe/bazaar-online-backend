namespace BazaarOnline.Domain.Entities.Features;

public class FeatureSelectType
{
    public int Id { get; set; }

    public string Options { get; set; }

    public IEnumerable<string> OptionsList => Options.Split('\u002C' /* , */);

    #region Relations

    public IEnumerable<Feature> Features { get; set; }

    #endregion
}