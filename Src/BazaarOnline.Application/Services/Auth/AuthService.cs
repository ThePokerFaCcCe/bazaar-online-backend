using BazaarOnline.Application.DTOs.AuthDTOs;
using BazaarOnline.Application.DTOs.JwtDTOs;
using BazaarOnline.Application.Generators;
using BazaarOnline.Application.Interfaces.Auth;
using BazaarOnline.Application.Securities;
using BazaarOnline.Domain.Entities.Users;
using BazaarOnline.Domain.Interfaces;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;

namespace BazaarOnline.Application.Services.Auth
{
    public class AuthService : IAuthService
    {
        private readonly IRepository _repository;
        private readonly IConfiguration _configuration;
        private readonly IWebHostEnvironment _env;

        public AuthService(IRepository repository, IConfiguration configuration, IWebHostEnvironment env)
        {
            _repository = repository;
            _configuration = configuration;
            _env = env;
        }


        public GeneratedTokenDTO CreateJwtToken(User user)
        {
            string issuer = _configuration["JwtSettings:Issuer"];
            string encKey = System.Environment.GetEnvironmentVariable("JWT__SIGNKEY");
            string signKey = System.Environment.GetEnvironmentVariable("JWT__ENCRYPTKEY");
            int expireMinutes = _configuration.GetValue<int>("JwtSettings:ExpireMinutes");

            return JwtAuthorization.GenerateToken(user, issuer, signKey, encKey, expireMinutes);
        }

        public CodeSentResultDTO SendLoginUserEmail(User user)
        {
            int expireMinutes = _configuration.GetValue<int>("Settings:ValidationCodeExpireMinutes:Email", 1);
            var activeCode = _repository.Add<ValidationCode>(new ValidationCode
            {
                UserId = user.Id,
                Code = StringGenerator.GenerateActiveCode(),
                Type = ActiveCodeType.UserLogin,
                ExpireDate = DateTime.Now.AddMinutes(expireMinutes),
            });
            _repository.Save();

            return new CodeSentResultDTO
            {
                Message = $"کد تایید به ایمیل {user.Email} ارسال شد",
                ExpireDate = activeCode.ExpireDate,
            };
        }

        public ValidatedUserCodeResultDTO? ValidateUserEmailLoginCode(UserLoginByEmailDTO loginDTO,
            ModelStateDictionary ModelState)
        {
            var user = _repository.GetAll<User>()
                .Include(u => u.ValidationCodes)
                .SingleOrDefault(u => u.Email.ToLower() == loginDTO.Email.ToLower());

            if (user == null)
            {
                ModelState.AddModelError(nameof(loginDTO.Email), "حساب کاربری با این ایمیل یافت نشد");
                return null;
            }

            if (user.IsDeleted)
            {
                ModelState.AddModelError(nameof(loginDTO.Email), "حساب کاربری حذف شده است");
                return null;
            }


            var activeCode =
                user.ValidationCodes.FirstOrDefault(u =>
                    DateTime.Now < u.ExpireDate && u.Type == ActiveCodeType.UserLogin);
            if (activeCode == null)
            {
                ModelState.AddModelError(nameof(loginDTO.Code),
                    "هیچ کد فعالی برای این حساب وجود ندارد. مجددا درخواست کد بدهید");
                return null;
            }

            var developmentCode = _configuration.GetValue<string>("Settings:DefaultValidationCode", "123456");
            if (activeCode.Code != loginDTO.Code && _env.IsDevelopment() && loginDTO.Code != developmentCode)
            {
                ModelState.AddModelError(nameof(loginDTO.Code), "کد وارد شده معتبر نیست");
                return null;
            }

            return new ValidatedUserCodeResultDTO()
            {
                User = user,
                ValidationCode = activeCode,
            };
        }
    }
}