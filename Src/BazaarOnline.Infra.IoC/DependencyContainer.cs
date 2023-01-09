using BazaarOnline.Application.Interfaces.Auth;
using BazaarOnline.Application.Interfaces.Categories;
using BazaarOnline.Application.Interfaces.Users;
using BazaarOnline.Application.Services.Auth;
using BazaarOnline.Application.Services.Categories;
using BazaarOnline.Application.Services.Users;
using BazaarOnline.Domain.Interfaces;
using BazaarOnline.Infra.Data.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace BazaarOnline.Infra.IoC
{
    public class DependencyContainer
    {
        public static void RegisterService(IServiceCollection services)
        {
            #region Services
            services.AddScoped<IAuthService, AuthService>();

            #region Users
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IUserDashboardService, UserDashboardService>();
            services.AddScoped<IValidationCodeService, ValidationCodeService>();
            #endregion

            #region Categories

            services.AddScoped<ICategoryService, CategoryService>();

            #endregion

            #endregion

            #region Repositories
            services.AddScoped<IRepository, Repository>();
            #endregion
        }
    }
}
