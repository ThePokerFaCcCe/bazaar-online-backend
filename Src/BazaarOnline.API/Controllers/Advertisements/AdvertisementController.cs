using BazaarOnline.Application.DTOs.AdvertisementDTOs;
using BazaarOnline.Application.DTOs.MapDTOs;
using BazaarOnline.Application.Interfaces.Advertisements;
using BazaarOnline.Application.Interfaces.Categories;
using BazaarOnline.Application.Interfaces.Features;
using BazaarOnline.Application.Interfaces.Maps;
using BazaarOnline.Application.Interfaces.UploadCenter;
using BazaarOnline.Application.ViewModels.Categories;
using BazaarOnline.Domain.Entities.UploadCenter;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BazaarOnline.API.Controllers.Advertisements
{
    [Route("api/v1/advertisements")]
    [ApiController]
    public class AdvertisementController : ControllerBase
    {
        private readonly IAdvertisementService _advertisementService;
        private readonly IMapService _mapService;
        private readonly ICategoryService _categoryService;
        private readonly IFeatureHandlerService _featureHandlerService;
        private readonly IFileCenterService _fileCenterService;

        public AdvertisementController(IAdvertisementService advertisementService, IMapService mapService,
            ICategoryService categoryService, IFeatureHandlerService featureHandlerService,
            IFileCenterService fileCenterService)
        {
            _advertisementService = advertisementService;
            _mapService = mapService;
            _categoryService = categoryService;
            _featureHandlerService = featureHandlerService;
            _fileCenterService = fileCenterService;
        }

        [HttpGet("")]
        public IActionResult GetAdvertisementList([FromQuery] AdvertisemenFilterDTO filterDto)
        {
            return Ok(_advertisementService.GetAdvertisementList(filterDto));
        }

        [Authorize]
        [HttpPost("")]
        public IActionResult CreateAdvertisement([FromBody] CreateAdvertisementDTO dto)
        {
            if (!ModelState.IsValid) return BadRequest(dto);

            bool hasErrors = false;
            if (!_categoryService.IsCategoryExists(dto.CategoryId))
            {
                ModelState.AddModelError(nameof(dto.CategoryId), "دسته بندی یافت نشد");
                hasErrors = true;
            }
            else if (_categoryService.GetCategoryType(dto.CategoryId) != CategoryTreeNodeTypeEnum.Leaf)
            {
                ModelState.AddModelError(nameof(dto.CategoryId), "در این دسته بندی امکان ساخت آگهی وجود ندارد");
                hasErrors = true;
            }

            var pictureValidation =
                _fileCenterService.ValidateFileTypes(dto.Pictures, FileCenterTypeEnum.AdvertisementPicture);
            if (!pictureValidation.IsSuccess)
            {
                ModelState.AddModelError(nameof(dto.Pictures), pictureValidation.Message);
                hasErrors = true;
            }

            var locationValidation =
                _mapService.ValidateLocation(dto.ProvinceId, dto.CityId, dto.Longitude, dto.Latitude);
            if (!locationValidation.IsValid)
            {
                switch (locationValidation.Status)
                {
                    case LocationValidationStatusEnum.ProvinceNotFound:
                        ModelState.AddModelError(nameof(dto.ProvinceId), locationValidation.Message);
                        break;
                    case LocationValidationStatusEnum.CityNotFound:
                        ModelState.AddModelError(nameof(dto.CityId), locationValidation.Message);
                        break;
                    case LocationValidationStatusEnum.CoordinatesNotInProvince:
                        ModelState.AddModelError(nameof(dto.Latitude), locationValidation.Message);
                        ModelState.AddModelError(nameof(dto.Longitude), locationValidation.Message);
                        break;
                }

                hasErrors = true;
            }

            var featureValidation = _featureHandlerService.ValidateAdvertisementFeatures(dto.CategoryId, dto.Features);
            if (!featureValidation.IsSuccess)
            {
                ModelState.AddModelError(nameof(dto.Features), featureValidation.Message);
                hasErrors = true;
            }

            if (hasErrors) return ValidationProblem(ModelState);

            var advertisementId = _advertisementService.CreateAdvertisement(dto, User.Identity.Name);
            return Created("https://google.com", new { Id = advertisementId });
            //return CreatedAtAction("", new { Id = advertisementId });
        }
    }
}