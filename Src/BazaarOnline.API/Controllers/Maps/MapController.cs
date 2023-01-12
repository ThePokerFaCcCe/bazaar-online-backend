using BazaarOnline.Application.DTOs.MapDTOs;
using BazaarOnline.Application.Interfaces.Maps;
using BazaarOnline.Application.ViewModels.Maps;
using Microsoft.AspNetCore.Mvc;

namespace BazaarOnline.API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class MapController : ControllerBase
    {
        private readonly IMapService _mapService;

        public MapController(IMapService mapService)
        {
            _mapService = mapService;
        }


        [HttpGet("Province")]
        public ActionResult<IEnumerable<ProvinceListDetailViewModel>> GetProvinceList()
        {
            return Ok(_mapService.GetProvincesList());
        }


        [HttpPost("Province/Filter")]
        public ActionResult<IEnumerable<ProvinceListDetailViewModel>> GetProvinceFilteredList(
            [FromBody] ProvinceFilterDTO dto)
        {
            if (!ModelState.IsValid) return BadRequest(dto);

            return Ok(_mapService.FindProvinces(dto));
        }


        [HttpGet("Province/{provinceId}/Cities")]
        public ActionResult<IEnumerable<ProvinceListDetailViewModel>> GetProvinceCitiesList(int provinceId)
        {
            if (!_mapService.IsProvinceExists(provinceId)) return NotFound();

            return Ok(_mapService.GetCitiesList(provinceId));
        }


        [HttpPost("Province/{provinceId}/Cities/Filter")]
        public ActionResult<IEnumerable<ProvinceListDetailViewModel>> GetProvinceCitiesFilteredList(
            int provinceId, [FromBody] CityFilterDTO dto)
        {
            if (!ModelState.IsValid) return BadRequest(dto);
            if (!_mapService.IsProvinceExists(provinceId)) return NotFound();

            return Ok(_mapService.FindCities(provinceId, dto));
        }
    }
}