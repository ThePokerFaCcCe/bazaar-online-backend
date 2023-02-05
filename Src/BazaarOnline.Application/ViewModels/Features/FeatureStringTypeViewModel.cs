namespace BazaarOnline.Application.ViewModels.Features;

public class FeatureStringTypeViewModel
{
    public int MinLength { get; set; }

    public int MaxLength { get; set; }

    public string? Regex { get; set; }

    public string Placeholder { get; set; }
}