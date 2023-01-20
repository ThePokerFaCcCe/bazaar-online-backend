using System.ComponentModel.DataAnnotations;

namespace BazaarOnline.Domain.Entities.Advertisements;

public enum AdvertisementStatusTypeEnum
{
    [Display(Name = "در انتظار تایید")] Pending = 1,

    [Display(Name = "تایید شده")] Accepted = 2,

    [Display(Name = "رد شده")] Declined = 3,

    [Display(Name = "حذف شده توسط کاربر")] DeletedByUser = 4,

    [Display(Name = "حذف شده توسط مدیر")] DeletedByAdmin = 5,
}