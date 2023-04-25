using BazaarOnline.Application.Interfaces.Users;
using BazaarOnline.Application.Utils.Extensions;
using BazaarOnline.Application.ViewModels.Users.UserDashboardViewModels;
using BazaarOnline.Domain.Entities.Users;
using BazaarOnline.Domain.Interfaces;
using System.Security.Claims;
using BazaarOnline.Application.DTOs.UserDashboardDTOs;

namespace BazaarOnline.Application.Services.Users
{
    public class UserDashboardService : IUserDashboardService
    {
        private readonly IRepository _repository;

        public UserDashboardService(IRepository repository)
        {
            _repository = repository;
        }

        public User? GetAuthorizedUser(ClaimsPrincipal user)
        {
            return _repository.Get<User>(user.Identity.Name);
        }

        public UserShortDashboardDetailViewModel? GetUserShortDetail(string userId)
        {
            var user = _repository.Get<User>(userId);
            if (user == null) return null;

            var result = new UserShortDashboardDetailViewModel
            {
                Data=new UserShortDashboardDataDetailViewModel
                {

                }.FillFromObject(user),
            }.FillFromObject(user);
            return result;
        }

        public UserShortDashboardDetailViewModel? UpdateUserDashboardDetail(string userId,
            UpdateUserDashboardDetailDTO dto)
        {
            var user = _repository.Get<User>(userId);
            if (user == null) return null;

            dto.TrimStrings();
            user.FillFromObject(dto);

            _repository.Update(user);
            _repository.Save();

            var result = new UserShortDashboardDetailViewModel();
            return result.FillFromObject(user);
        }
    }
}