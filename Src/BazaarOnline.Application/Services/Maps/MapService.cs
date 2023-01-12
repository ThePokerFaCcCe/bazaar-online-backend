using BazaarOnline.Application.DTOs.MapDTOs;
using BazaarOnline.Application.Filters;
using BazaarOnline.Application.Interfaces.Maps;
using BazaarOnline.Application.Utils.Extentions;
using BazaarOnline.Application.ViewModels.Maps;
using BazaarOnline.Domain.Entities.Maps;
using BazaarOnline.Domain.Interfaces;

namespace BazaarOnline.Application.Services.Maps
{
    public class MapService : IMapService
    {
        private readonly IRepository _repository;

        public MapService(IRepository repository)
        {
            _repository = repository;
        }

        public IEnumerable<ProvinceListDetailViewModel> GetProvincesList()
        {
            return _repository.GetAll<Province>()
                .Select(p => new ProvinceListDetailViewModel().FillFromObject(p, false));
        }

        public IEnumerable<ProvinceListDetailViewModel> FindProvinces(ProvinceFilterDTO filterDto)
        {
            filterDto.TrimStrings();
            return _repository.GetAll<Province>()
                .Filter(filterDto)
                .Select(p => new ProvinceListDetailViewModel().FillFromObject(p, false));
        }

        public bool IsProvinceExists(int provinceId)
        {
            return _repository.GetAll<Province>()
                .Any(p => p.Id == provinceId);
        }

        public IEnumerable<CityListDetailViewModel> GetCitiesList(int provinceId)
        {
            return _repository.GetAll<City>()
                .Where(c => c.ProvinceId == provinceId)
                .Select(c => new CityListDetailViewModel().FillFromObject(c, false));
        }

        public IEnumerable<CityListDetailViewModel> FindCities(int provinceId, CityFilterDTO filterDto)
        {
            filterDto.TrimStrings();
            return _repository.GetAll<City>()
                .Where(c => c.ProvinceId == provinceId)
                .Filter(filterDto)
                .Select(c => new CityListDetailViewModel().FillFromObject(c, false));
        }
    }
}