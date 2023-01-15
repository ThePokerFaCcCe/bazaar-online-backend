using System.ComponentModel.DataAnnotations;

namespace BazaarOnline.Application.ViewModels.Maps;

public enum LocationTypeEnum
{
    [Display(Name = "Province")] Province = 1,

    [Display(Name = "City")] City = 2,
}