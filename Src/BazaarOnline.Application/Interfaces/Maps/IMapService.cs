using BazaarOnline.Application.DTOs.MapDTOs;
using BazaarOnline.Application.ViewModels.Maps;

namespace BazaarOnline.Application.Interfaces.Maps
{
    public interface IMapService
    {
        #region Province

        IEnumerable<ProvinceListDetailViewModel> GetProvincesList();
        IEnumerable<ProvinceListDetailViewModel> FindProvinces(ProvinceFilterDTO filterDto);

        bool IsProvinceExists(int provinceId);

        #endregion

        #region City

        IEnumerable<CityListDetailViewModel> GetCitiesList(int provinceId);
        IEnumerable<CityListDetailViewModel> FindCities(int provinceId, CityFilterDTO filterDto);

        #endregion
    }
}