using BazaarOnline.Application.Filters.Generic.Attributes;

namespace BazaarOnline.Application.Utils.Attributes;

[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false)]
public class FillerPropertyAttribute : Attribute
{
    /// <summary>
    /// <b>Used for '.FillFromObject()' extension method</b><br></br>
    /// Fill property from this propertyName in filler object
    /// </summary>
    public FillerPropertyAttribute()
    {
        if (!Ignore && PropertyName == null)
        {
            throw new ArgumentNullException("`PropertyName` is required when `Ignore` is false");
        }
    }

    /// <summary>
    /// Property name in filler object
    /// </summary>
    public string PropertyName { get; set; }

    /// <summary>
    /// Ignore this property from filling
    /// </summary>
    public bool Ignore { get; set; } = false;
}