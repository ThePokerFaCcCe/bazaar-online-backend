using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace BazaarOnline.Application.DTOs.AuthDTOs;

public class UserLoginByPhoneNumberDTO
{
    [DisplayName("شماره همراه")]
    [Required(ErrorMessage = "این فیلد اجباری است")]
    [StringLength(maximumLength: 11, MinimumLength = 11, ErrorMessage = "{0} باید {1} کاراکتر باشد")]
    [RegularExpression(@"^(0)9(0[1-5]|[1 3 9]\d|2[0-2]|98)\d{7}$", ErrorMessage = "لطفا یک شماره همراه معتبر وارد کنید")]
    public string PhoneNumber { get; set; }

    [DisplayName("کد تایید")]
    [Required(ErrorMessage = "این فیلد اجباری است")]
    [StringLength(6, MinimumLength = 6, ErrorMessage = "{0} باید {1} کاراکتر باشد")]
    public string Code { get; set; }
}