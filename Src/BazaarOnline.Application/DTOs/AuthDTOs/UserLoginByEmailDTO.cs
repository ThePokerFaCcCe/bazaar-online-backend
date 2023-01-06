using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace BazaarOnline.Application.DTOs.AuthDTOs
{
    public class UserLoginByEmailDTO
    {
        [DisplayName("ایمیل")]
        [Required(ErrorMessage = "این فیلد اجباری است")]
        [MaxLength(100, ErrorMessage = "{0} باید حداکثر {1} کاراکتر باشد")]
        [EmailAddress(ErrorMessage = "لطفا یک ایمیل معتبر وارد کنید")]
        public string Email { get; set; }

        [DisplayName("کد تایید")]
        [Required(ErrorMessage = "این فیلد اجباری است")]
        [StringLength(6, MinimumLength = 6, ErrorMessage = "{0} باید {1} کاراکتر باشد")]
        public string Code { get; set; }
    }
}
