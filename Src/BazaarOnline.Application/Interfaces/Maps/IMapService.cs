﻿using BazaarOnline.Application.DTOs.MapDTOs;
using BazaarOnline.Application.ViewModels.Maps;

namespace BazaarOnline.Application.Interfaces.Maps
{
    public interface IMapService
    {
        #region Province

        IEnumerable<ProvinceListDetailViewModel> GetProvinces();

        bool IsProvinceExists(int provinceId);

        #endregion

        #region City

        IEnumerable<CityListDetailViewModel> GetProvinceCities(int provinceId);
        IEnumerable<CityListDetailViewModel> GetCitiesFilterList(CityFilterDTO filterDTO);

        #endregion

        IEnumerable<LocationListViewModel> FindLocation(MapSearchDTO searchDto);

        LocationValidationResultDTO ValidateLocation(int provinceId, int cityId, double longitude, double latitude);
    }
}