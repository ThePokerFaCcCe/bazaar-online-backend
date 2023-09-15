using BazaarOnline.Application.DTOs.MapDTOs;
using BazaarOnline.Application.Interfaces.Maps;
using BazaarOnline.Application.ViewModels.Maps;
using Microsoft.AspNetCore.Mvc;

namespace BazaarOnline.API.Controllers
{
    [Route("api/v1/map")]
    [ApiController]
    public class MapController : ControllerBase
    {
        private readonly IMapService _mapService;

        public MapController(IMapService mapService)
        {
            _mapService = mapService;
        }

        [HttpGet("provinces")]
        public ActionResult<IEnumerable<ProvinceListDetailViewModel>> GetProvinceList()
        {
            return Ok(_mapService.GetProvinces());
        }

        [HttpGet("provinces/{provinceId}/cities")]
        public ActionResult<IEnumerable<ProvinceListDetailViewModel>> GetProvinceCitiesList(int provinceId)
        {
            if (!_mapService.IsProvinceExists(provinceId)) return NotFound();

            return Ok(_mapService.GetProvinceCities(provinceId));
        }

        [HttpPost("cities/filter")]
        public ActionResult<IEnumerable<CityListDetailViewModel>> GetCityList([FromBody]CityFilterDTO dto)
        {
            if (!ModelState.IsValid) return BadRequest();

            return Ok(_mapService.GetCitiesFilterList(dto));
        }

        [HttpGet("find")]
        public ActionResult<IEnumerable<LocationListViewModel>> FindLocation([FromQuery] MapSearchDTO dto)
        {
            return Ok(_mapService.FindLocation(dto));
        }
    }
}