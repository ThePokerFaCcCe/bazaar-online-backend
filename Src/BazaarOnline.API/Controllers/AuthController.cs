using BazaarOnline.Application.DTOs.AuthDTOs;
using BazaarOnline.Application.DTOs.JwtDTOs;
using BazaarOnline.Application.DTOs.UserDTOs;
using BazaarOnline.Application.Interfaces.Auth;
using BazaarOnline.Application.Interfaces.Users;
using Microsoft.AspNetCore.Mvc;

namespace BazaarOnline.API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        private readonly IUserService _userService;
        private readonly IValidationCodeService _validationCodeService;

        public AuthController(IAuthService authService, IUserService userService, IValidationCodeService validationCodeService)
        {
            _authService = authService;
            _userService = userService;
            _validationCodeService = validationCodeService;
        }

        [HttpPost(nameof(LoginByEmail))]
        public ActionResult<GeneratedTokenDTO> LoginByEmail(UserLoginByEmailDTO dto)
        {
            if (!ModelState.IsValid) return BadRequest(dto);

            var validationResult = _authService.ValidateUserEmailLoginCode(dto, ModelState);
            if (validationResult == null) return ValidationProblem(ModelState);

            _validationCodeService.DeleteValidationCode(validationResult.ValidationCode);
            _userService.ActivateUser(validationResult.User);

            var token = _authService.CreateJwtToken(validationResult.User);
            return Ok(token);
        }

        [HttpPost(nameof(SendEmailLoginCode))]
        public ActionResult<CodeSentResultDTO> SendEmailLoginCode(SendEmailLoginCodeDTO dto)
        {
            if (!ModelState.IsValid) return BadRequest(dto);

            var user = _userService.FindUserByEmail(dto.Email);
            if (user == null)
            {
                user = _userService.CreateUserByEmail(
                    new CreateUserByEmailDTO()
                    {
                        Email = dto.Email
                    });
            }
            else if (_validationCodeService.IsActiveEmailValidationExists(user.Id))
            {
                ModelState.AddModelError(nameof(dto.Email),"کد قبلا برای این ایمیل ارسال شده است.");
                return ValidationProblem(ModelState);
            }

            var activationResult = _authService.SendLoginUserEmail(user);
            return Ok(activationResult);
        }
    }
}
