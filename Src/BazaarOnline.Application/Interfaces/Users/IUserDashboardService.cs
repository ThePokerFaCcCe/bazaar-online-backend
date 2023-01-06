using System.Security.Claims;
using BazaarOnline.Application.ViewModels.Users.UserDashboardViewModels;
using BazaarOnline.Domain.Entities.Users;

namespace BazaarOnline.Application.Interfaces.Users
{
    public interface IUserDashboardService
    {
        User? GetAuthorizedUser(ClaimsPrincipal User);
        UserShortDashboardDetailViewModel? GetUserShortDetail(string userId);
    }
}
