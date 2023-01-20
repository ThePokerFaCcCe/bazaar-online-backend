using System.ComponentModel.DataAnnotations;

namespace BazaarOnline.Domain.Entities.Advertisements;

public enum AdvertisementContactTypeEnum
{
    [Display(Name = "تماس و چت")] Normal = 1,
    [Display(Name = "فقط چت")] ChatOnly = 2,
    [Display(Name = "فقط تماس")] CallOnly = 3,
}