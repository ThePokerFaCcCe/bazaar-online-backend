namespace BazaarOnline.Application.ViewModels.Features;

public class FeatureSelectTypeViewModel
{
    public string Options { get; set; }

    public IEnumerable<string> OptionsList => Options.Split('\u002C' /* , */);
}