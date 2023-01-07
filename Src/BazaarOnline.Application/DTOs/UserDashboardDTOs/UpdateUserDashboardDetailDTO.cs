using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BazaarOnline.Application.DTOs.UserDashboardDTOs
{
    public class UpdateUserDashboardDetailDTO
    {
        [DisplayName("نام نمایشی")]
        [Required(ErrorMessage = "این فیلد اجباری است")]
        [StringLength(60, MinimumLength = 4, ErrorMessage = "{0} باید بین {1} و {2} کاراکتر باشد")]
        public string DisplayName { get; set; }
    }
}
