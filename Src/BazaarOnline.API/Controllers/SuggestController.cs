using BazaarOnline.Application.DTOs.AdvertisementDTOs;
using BazaarOnline.Application.Interfaces.Advertisements;
using Microsoft.AspNetCore.Mvc;

namespace BazaarOnline.API.Controllers
{
    [Route("api/v1/suggest")]
    [ApiController]
    public class SuggestController : ControllerBase
    {
        private readonly IAdvertisementService _advertisementService;

        public SuggestController(IAdvertisementService advertisementService)
        {
            _advertisementService = advertisementService;
        }

        [HttpGet("")]
        public IActionResult GetAdvertisementList([FromQuery] AdvertisementSearchDTO searchDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(searchDto);
            }

            return Ok(_advertisementService.SearchSuggestAdvertisement(searchDto));
        }
    }
}