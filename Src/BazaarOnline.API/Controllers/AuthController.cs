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

        public AuthController(IAuthService authService, IUserService userService,
            IValidationCodeService validationCodeService)
        {
            _authService = authService;
            _userService = userService;
            _validationCodeService = validationCodeService;
        }

        [HttpPost(nameof(LoginByPhoneNumber))]
        public ActionResult<GeneratedTokenDTO> LoginByPhoneNumber(UserLoginByPhoneNumberDTO dto)
        {
            if (!ModelState.IsValid) return BadRequest(dto);

            var validationResult = _authService.ValidateUserPhoneNumberLoginCode(dto, ModelState);
            if (validationResult == null) return ValidationProblem(ModelState);

            //_validationCodeService.DeleteValidationCode(validationResult.ValidationCode);
            _userService.ActivateUser(validationResult.User);

            var token = _authService.CreateJwtToken(validationResult.User);
            return Ok(token);
        }


        [HttpPost(nameof(LoginByEmail))]
        public ActionResult<GeneratedTokenDTO> LoginByEmail(UserLoginByEmailDTO dto)
        {
            if (!ModelState.IsValid) return BadRequest(dto);

            var validationResult = _authService.ValidateUserEmailLoginCode(dto, ModelState);
            if (validationResult == null) return ValidationProblem(ModelState);

            //_validationCodeService.DeleteValidationCode(validationResult.ValidationCode);
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
                ModelState.AddModelError(nameof(dto.Email), "کد قبلا برای این ایمیل ارسال شده است.");
                return ValidationProblem(ModelState);
            }

            var activationResult = _authService.SendLoginUserEmail(user);
            return Ok(activationResult);
        }

        [HttpPost(nameof(SendSMSLoginCode))]
        public ActionResult<CodeSentResultDTO> SendSMSLoginCode(SendSMSLoginCodeDTO dto)
        {
            if (!ModelState.IsValid) return BadRequest(dto);

            var user = _userService.FindUserByPhoneNumber(dto.PhoneNumber);
            if (user == null)
            {
                user = _userService.CreateUserByPhoneNumber(
                    new CreateUserByPhoneNumberDTO()
                    {
                        PhoneNumber = dto.PhoneNumber,
                    });
            }
            else if (_validationCodeService.IsActivePhoneNumberValidationExists(user.Id))
            {
                ModelState.AddModelError(nameof(dto.PhoneNumber), "کد قبلا برای این شماره همراه ارسال شده است.لطفا دقایقی دیگر تلاش کنید.");
                return ValidationProblem(ModelState);
            }

            var activationResult = _authService.SendLoginUserSMS(user);
            return Ok(activationResult);
        }
    }
}