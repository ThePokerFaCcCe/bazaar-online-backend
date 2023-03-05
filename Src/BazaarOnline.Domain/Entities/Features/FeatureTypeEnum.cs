using System.ComponentModel.DataAnnotations;

namespace BazaarOnline.Domain.Entities.Features;

public enum FeatureTypeEnum
{
    [Display(Name = "text")] String = 1,
    [Display(Name = "number")] Integer = 2,
    [Display(Name = "select")] Select = 3,
}