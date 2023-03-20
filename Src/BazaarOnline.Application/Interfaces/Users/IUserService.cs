using BazaarOnline.Application.DTOs.UserDTOs;
using BazaarOnline.Domain.Entities.Users;

namespace BazaarOnline.Application.Interfaces.Users
{
    public interface IUserService
    {
        #region Create

        User CreateUserByEmail(CreateUserByEmailDTO dto);

        #endregion

        #region Find

        User? FindUserByEmail(string email);

        bool IsUserExistsByEmail(string email);

        #endregion

        #region Update

        /// <summary>
        /// set user's `IsActive` to true (only if it's not true currently)
        /// </summary>
        /// <param name="user"></param>
        void ActivateUser(User user);

        #endregion
    }
}