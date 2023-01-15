using BazaarOnline.Application.DTOs.UserDashboardDTOs;
using BazaarOnline.Application.Interfaces.Users;
using BazaarOnline.Application.ViewModels.Users.UserDashboardViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BazaarOnline.API.Controllers.Users
{
    [Route("api/v1/users/me")]
    [ApiController]
    [Authorize]
    public class UserDashboardController : ControllerBase
    {
        private readonly IUserDashboardService _userDashboardService;

        public UserDashboardController(IUserDashboardService userDashboardService)
        {
            _userDashboardService = userDashboardService;
        }

        [HttpGet("info")]
        public ActionResult<UserShortDashboardDetailViewModel> GetUserShortDetail()
        {
            return Ok(_userDashboardService.GetUserShortDetail(User.Identity.Name));
        }

        [HttpPut("")]
        public ActionResult<UserShortDashboardDetailViewModel> UpdateUserDashboardDetail(
            UpdateUserDashboardDetailDTO dto)
        {
            if (!ModelState.IsValid) return BadRequest(dto);

            return Ok(_userDashboardService.UpdateUserDashboardDetail(User.Identity.Name, dto));
        }
    }
}