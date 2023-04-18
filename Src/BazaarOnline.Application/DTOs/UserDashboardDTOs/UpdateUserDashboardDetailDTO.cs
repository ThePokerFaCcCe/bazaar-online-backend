using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace BazaarOnline.Application.DTOs.UserDashboardDTOs
{
    public class UpdateUserDashboardDetailDTO
    {
        [DisplayName("نام نمایشی")]
        [Required(ErrorMessage = "این فیلد اجباری است")]
        [StringLength(60, MinimumLength = 4, ErrorMessage = "{0} باید بین {1} و {2} کاراکتر باشد")]
        public string DisplayName { get; set; }

        [DisplayName("ساعت ضروع پاسخ دهی")]
        [Required(ErrorMessage = "این فیلد اجباری است")]
        [Range(0, 23, ErrorMessage = "باید عددی بین 0 تا 23 وارد کنید")]
        public int AnswerHourStart { get; set; }

        [DisplayName("ساعت پایان پاسخ دهی")]
        [Required(ErrorMessage = "این فیلد اجباری است")]
        [Range(0, 23, ErrorMessage = "باید عددی بین 0 تا 23 وارد کنید")]
        public int AnswerHourEnd { get; set; }
    }
}
