using BazaarOnline.Application.DTOs.MapDTOs;
using BazaarOnline.Application.Filters;
using BazaarOnline.Application.Interfaces.Maps;
using BazaarOnline.Application.Requesters;
using BazaarOnline.Application.Utils.Extensions;
using BazaarOnline.Application.ViewModels.Maps;
using BazaarOnline.Domain.Entities.Maps;
using BazaarOnline.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BazaarOnline.Application.Services.Maps
{
    public class MapService : IMapService
    {
        private readonly IRepository _repository;

        public MapService(IRepository repository)
        {
            _repository = repository;
        }

        public IEnumerable<ProvinceListDetailViewModel> GetProvinces()
        {
            return _repository.GetAll<Province>()
                .Include(p => p.Cities)
                .Select(p => new ProvinceListDetailViewModel { CitiesCount = p.Cities.Count() }.FillFromObject(p, false));
        }

        public bool IsProvinceExists(int provinceId)
        {
            return _repository.GetAll<Province>()
                .Any(p => p.Id == provinceId);
        }

        public IEnumerable<CityListDetailViewModel> GetProvinceCities(int provinceId)
        {
            return _repository.GetAll<City>()
                .Where(c => c.ProvinceId == provinceId)
                .Select(c => new CityListDetailViewModel().FillFromObject(c, false));
        }

        public IEnumerable<CityListDetailViewModel> GetCitiesFilterList(CityFilterDTO filterDTO)
        {
            var cities = _repository.GetAll<City>()
                .Filter(filterDTO)
                .Select(c => new CityListDetailViewModel().FillFromObject(c, false));
            return cities;
        }

        public IEnumerable<LocationListViewModel> FindLocation(MapSearchDTO filterDto)
        {
            filterDto.TrimStrings();

            var provinceList = _repository.GetAll<Province>()
                .Filter(filterDto)
                .Select(p => new LocationListViewModel
                {
                    LocationTypeId = LocationTypeEnum.Province
                }.FillFromObject(p, false));

            var cityList = _repository.GetAll<City>()
                .Filter(filterDto)
                .Select(c => new LocationListViewModel
                {
                    LocationTypeId = LocationTypeEnum.City
                }.FillFromObject(c, false));


            var resultList = new List<LocationListViewModel>();
            resultList.AddRange(provinceList);
            resultList.AddRange(cityList);

            return resultList.OrderBy(v => v.LocationTypeId);
        }

        public LocationValidationResultDTO ValidateLocation(int provinceId, int cityId, double longitude,
            double latitude)
        {
            var province = _repository.GetAll<Province>()
                .SingleOrDefault(p => p.Id == provinceId);

            if (province == null)
            {
                return new LocationValidationResultDTO
                {
                    Status = LocationValidationStatusEnum.ProvinceNotFound,
                    Message = "استان یافت نشد",
                };
            }

            bool isCityExists = _repository.GetAll<City>()
                .Any(c => c.Id == cityId && c.ProvinceId == provinceId);

            if (!isCityExists)
            {
                return new LocationValidationResultDTO
                {
                    Status = LocationValidationStatusEnum.CityNotFound,
                    Message = "شهر یافت نشد",
                };
            }

            bool isValidCoordinates = false;
            try
            {
                isValidCoordinates =
                    ReverseGeocoding.IsCoordinateInsideProvince(province.AmarCode, latitude, longitude);
            }
            catch (Exception e)
            {
                return new LocationValidationResultDTO
                {
                    Status = LocationValidationStatusEnum.ServerError,
                    Message = $"خطا در برقراری ارتباط با سرور اعتبارسنجی مختصات. - {e.Message}",
                };
            }

            if (!isValidCoordinates)
            {
                return new LocationValidationResultDTO
                {
                    Status = LocationValidationStatusEnum.CoordinatesNotInProvince,
                    Message = $"مختصات منطقه انتخاب شده داخل استان {province.Name} نیست",
                };
            }

            return new LocationValidationResultDTO
            {
                Status = LocationValidationStatusEnum.IsValid
            };
        }
    }
}