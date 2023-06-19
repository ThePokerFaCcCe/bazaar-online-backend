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
        private readonly IUserAdvertisementService _userAdvertisementService;

        public UserDashboardController(IUserDashboardService userDashboardService,
            IUserAdvertisementService userAdvertisementService)
        {
            _userDashboardService = userDashboardService;
            _userAdvertisementService = userAdvertisementService;
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
            if (dto.AnswerHourStart >= dto.AnswerHourEnd)
            {
                ModelState.AddModelError(nameof(dto.AnswerHourStart), "ساعت شروع باید کمتر از ساعت پایان باشد");
                return ValidationProblem(ModelState);
            }

            return Ok(_userDashboardService.UpdateUserDashboardDetail(User.Identity.Name, dto));
        }

        [HttpGet("bookmarks")]
        public IActionResult GetUserBookmarks()
        {
            var userId = User.Identity.Name;
            var result = _userAdvertisementService.GetUserBookmarks(userId);
            return Ok(result);
        }

        [HttpGet("history")]
        public IActionResult GetUserHistory()
        {
            var userId = User.Identity.Name;
            var result = _userAdvertisementService.GetAdvertisementsHistory(userId);
            return Ok(result);
        }

        [HttpGet("notes")]
        public IActionResult GetUserNotes()
        {
            var userId = User.Identity.Name;
            var result = _userAdvertisementService.GetUserNotes(userId);
            return Ok(result);
        }
    }
}