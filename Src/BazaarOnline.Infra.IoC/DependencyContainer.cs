using BazaarOnline.Application.Interfaces.Advertisements;
using BazaarOnline.Application.Interfaces.Auth;
using BazaarOnline.Application.Interfaces.Categories;
using BazaarOnline.Application.Interfaces.Features;
using BazaarOnline.Application.Interfaces.Maps;
using BazaarOnline.Application.Interfaces.UploadCenter;
using BazaarOnline.Application.Interfaces.Users;
using BazaarOnline.Application.Services.Advertisements;
using BazaarOnline.Application.Services.Auth;
using BazaarOnline.Application.Services.Categories;
using BazaarOnline.Application.Services.Features;
using BazaarOnline.Application.Services.Maps;
using BazaarOnline.Application.Services.UploadCenter;
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

            #region Maps

            services.AddScoped<IMapService, MapService>();

            #endregion

            #region Advertisements

            services.AddScoped<IAdvertisementService, AdvertisementService>();

            #endregion

            #region Features

            services.AddScoped<IFeatureHandlerService, FeatureHandlerService>();

            #endregion

            #region UploadCenter

            services.AddScoped<IFileCenterService, FileCenterService>();

            #endregion

            #endregion

            #region Repositories

            services.AddScoped<IRepository, Repository>();

            #endregion
        }
    }
}