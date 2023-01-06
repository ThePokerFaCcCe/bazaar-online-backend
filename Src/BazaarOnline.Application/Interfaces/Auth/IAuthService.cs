using BazaarOnline.Application.DTOs.AuthDTOs;
using BazaarOnline.Application.DTOs.JwtDTOs;
using BazaarOnline.Domain.Entities.Users;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace BazaarOnline.Application.Interfaces.Auth
{
    public interface IAuthService
    {
        /// <summary>
        /// Find user and validate it's credentials with `loginDTO`. if creds is valid,
        /// then User object will return. else null and errors will add to `ModelState`
        /// </summary>
        /// <param name="loginEmailDto">credentials that entered</param>
        /// <param name="ModelState">ModelState for adding validation error to it</param>
        /// <returns>User object if credentials is valid</returns>
        ValidatedUserCodeResultDTO? ValidateUserEmailLoginCode(UserLoginByEmailDTO loginEmailDto, ModelStateDictionary ModelState);

        CodeSentResultDTO SendLoginUserEmail(User user);

        #region JWT
        GeneratedTokenDTO CreateJwtToken(User user);
        #endregion
    }
}
