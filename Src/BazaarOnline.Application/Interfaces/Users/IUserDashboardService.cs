using System.Security.Claims;
using BazaarOnline.Application.DTOs.UserDashboardDTOs;
using BazaarOnline.Application.ViewModels.Users.UserDashboardViewModels;
using BazaarOnline.Domain.Entities.Users;

namespace BazaarOnline.Application.Interfaces.Users
{
    public interface IUserDashboardService
    {
        #region Get

        User? GetAuthorizedUser(ClaimsPrincipal User);
        UserShortDashboardDetailViewModel? GetUserShortDetail(string userId);

        #endregion

        #region Update

        UserShortDashboardDetailViewModel? UpdateUserDashboardDetail(string userId, UpdateUserDashboardDetailDTO dto);

        #endregion
    }
    }
