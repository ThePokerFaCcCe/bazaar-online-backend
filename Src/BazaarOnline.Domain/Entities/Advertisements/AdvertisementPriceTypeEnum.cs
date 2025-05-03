using System.ComponentModel.DataAnnotations;

namespace BazaarOnline.Domain.Entities.Advertisements;

public enum AdvertisementPriceTypeEnum
{
    [Display(Name = "توافقی")] Agreement = 1,

    [Display(Name = "دارای مبلغ")] Price = 2,
    [Display(Name = "فاقد مبلغ")] NoPrice = 3,
}