using BazaarOnline.Application.DTOs.AuthDTOs;
using BazaarOnline.Application.DTOs.JwtDTOs;
using BazaarOnline.Application.Generators;
using BazaarOnline.Application.Interfaces.Auth;
using BazaarOnline.Application.Requesters;
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
        private static int MAX_SMS_COUNT_DAILY
        {
            get
            {
                var envValue = System.Environment.GetEnvironmentVariable("MAX_SMS_COUNT_DAILY");

                var maxCount = 8;
                if (!int.TryParse(envValue, out maxCount))
                    maxCount = 8;

                return maxCount;
            }
        }

        private static int SendSMSCountToday = 0;
        private static DateTime LastSendSMSDate = DateTime.MinValue;

        private static bool CheckCanSendSMSToday()
        {
            if (DateTime.Now.Date > LastSendSMSDate.Date)
            {
                SendSMSCountToday = 0;
                LastSendSMSDate = DateTime.MinValue;
            }

            if (SendSMSCountToday > MAX_SMS_COUNT_DAILY)
                return false;
            return true;
        }
        private static void IncreaseSentSMSCount()
        {
            if (DateTime.Now.Date > LastSendSMSDate.Date)
            {
                SendSMSCountToday = 1;
            }

            SendSMSCountToday++;
            LastSendSMSDate = DateTime.Now;
        }


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

        public CodeSentResultDTO SendLoginUserSMS(User user)
        {
            // Check Last 2 Hours SMS Count
            int conf_lastHoursToCheck = _configuration.GetValue<int>("Settings:MaxSendValidationCodeOnMobile:LastHoursToCheck", 4);
            int conf_maxSentSMSCountInLastHours = _configuration.GetValue<int>("Settings:MaxSendValidationCodeOnMobile:MaxSentSMSCountInLastHours", 2);

            var lastDateToCheck = DateTime.Now.AddHours(conf_lastHoursToCheck * -1);
            var lastCodesCount = _repository.GetAll<ValidationCode>().IgnoreQueryFilters()
                .Count(v => v.UserId == user.Id && v.CreateDate >= lastDateToCheck);

            // delete old codes
            var oldCodes = _repository.GetAll<ValidationCode>().IgnoreQueryFilters().Where(v => v.UserId == user.Id && !v.IsDeleted);
            oldCodes.ToList().ForEach(v => v.Delete());
            _repository.UpdateRange(oldCodes);


            // create new code
            int expireMinutes = _configuration.GetValue<int>("Settings:ValidationCodeExpireMinutes:SMS", 2);
            var activeCode = _repository.Add<ValidationCode>(new ValidationCode
            {
                UserId = user.Id,
                Code = StringGenerator.GenerateActiveCode(),
                Type = ActiveCodeType.UserLogin,
                ExpireDate = DateTime.Now.AddMinutes(expireMinutes),
            });
            _repository.Save();

            if (!CheckCanSendSMSToday())
            {
                return new CodeSentResultDTO
                {
                    IsSuccess = false,
                    Message = $"محدودیت روزانه ارسال پیامک اعمال شده است!\n HINT: This project is built for our resume. so we don't want to send many login code messages!\n Anyways, This is your code if you're interested: {activeCode.Code}",
                    ExpireDate = activeCode.ExpireDate,
                };
            }

            if (lastCodesCount > conf_maxSentSMSCountInLastHours)
            {
                return new CodeSentResultDTO
                {
                    IsSuccess = false,
                    Message = $"محدودیت ارسال پیامک برای شماره همراه اعمال شده است!\n HINT: This project is built for our resume. so we don't want to send many login code messages!\n Anyways, This is your code if you're interested: {activeCode.Code}",
                    ExpireDate = activeCode.ExpireDate,
                };
            }

            try
            {
                TrezPanel.SendCodeSMS(user.PhoneNumber, activeCode.Code);
            }
            catch (Exception ex)
            {
                return new CodeSentResultDTO
                {
                    IsSuccess = false,
                    Message = $"خطایی در هنگام ارسال پیامک رخ داد! مجددا تلاش کنید\n HINT: This project is built for our resume. so, it's normal that the SMS Panel isn't working properly(for security reasons we can't tell why in here!)\n Anyways, This is your code if you're interested: {activeCode.Code}",
                    ExpireDate = activeCode.ExpireDate,
                };
            }

            IncreaseSentSMSCount();
            return new CodeSentResultDTO
            {
                IsSuccess = true,
                Message = $"کد تایید به شماره همراه {user.PhoneNumber} ارسال شد\n HINT: Sometimes SMS Panel may not work properly! so, if the SMS has not been sent to you yet, this is your code: {activeCode.Code}",
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
        public ValidatedUserCodeResultDTO? ValidateUserPhoneNumberLoginCode(UserLoginByPhoneNumberDTO loginDTO, ModelStateDictionary ModelState)
        {
            var user = _repository.GetAll<User>()
                .Include(u => u.ValidationCodes)
                .SingleOrDefault(u => u.PhoneNumber == loginDTO.PhoneNumber);

            if (user == null)
            {
                ModelState.AddModelError(nameof(loginDTO.PhoneNumber), "حساب کاربری با این شماره همراه یافت نشد");
                return null;
            }

            if (user.IsDeleted)
            {
                ModelState.AddModelError(nameof(loginDTO.PhoneNumber), "حساب کاربری حذف شده است");
                return null;
            }


            var activeCode =
                user.ValidationCodes.FirstOrDefault(u => u.Type == ActiveCodeType.UserLogin && !u.IsDeleted);

            if (activeCode?.IsExpired == true)
            {
                activeCode.Delete();
                _repository.Update(activeCode);
                _repository.Save();
            }

            if (activeCode == null || activeCode.IsDeleted)
            {
                ModelState.AddModelError(nameof(loginDTO.Code),
                    "هیچ کد فعالی برای این حساب وجود ندارد. مجددا درخواست کد بدهید");
                return null;
            }

            var developmentCode = _configuration.GetValue<string>("Settings:DefaultValidationCode", "123456");
            if (activeCode.Code != loginDTO.Code && _env.IsDevelopment() && loginDTO.Code != developmentCode)
            {
                activeCode.IncreaseTryCount();
                _repository.Update(activeCode);
                _repository.Save();

                ModelState.AddModelError(nameof(loginDTO.Code), "کد وارد شده معتبر نیست");
                return null;
            }

            activeCode.Delete();
            _repository.Update(activeCode);
            _repository.Save();

            return new ValidatedUserCodeResultDTO()
            {
                User = user,
                ValidationCode = activeCode,
            };
        }
    }
}