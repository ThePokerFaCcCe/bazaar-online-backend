using BazaarOnline.Application.Interfaces.Advertisements;
using Microsoft.AspNetCore.Mvc;

namespace BazaarOnline.API.Controllers
{
    [Route("api/v1/sitemap")]
    [ApiController]
    public class SitemapController : ControllerBase
    {
        private readonly IAdvertisementService _advertisementService;

        public SitemapController(IAdvertisementService advertisementService)
        {
            _advertisementService = advertisementService;
        }

        [HttpGet("")]
        public IActionResult GetSitemap()
        {
            return Ok(new
            {
                Advertisements = _advertisementService.GetAdvertisementSitemap(),
            });
        }
    }
}