using BazaarOnline.Application.DTOs;
using BazaarOnline.Application.DTOs.AdvertisementDTOs;
using BazaarOnline.Application.DTOs.MapDTOs;
using BazaarOnline.Application.DTOs.PaginationDTO;
using BazaarOnline.Application.Interfaces.Advertisements;
using BazaarOnline.Application.Interfaces.Categories;
using BazaarOnline.Application.Interfaces.Features;
using BazaarOnline.Application.Interfaces.Maps;
using BazaarOnline.Application.Interfaces.UploadCenter;
using BazaarOnline.Application.Interfaces.Users;
using BazaarOnline.Application.ViewModels.Categories;
using BazaarOnline.Application.ViewModels.Users;
using BazaarOnline.Domain.Entities.Advertisements;
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
        private readonly IUserAdvertisementService _userAdvertisementService;

        public AdvertisementController(IAdvertisementService advertisementService, IMapService mapService,
            ICategoryService categoryService, IFeatureHandlerService featureHandlerService,
            IFileCenterService fileCenterService, IUserAdvertisementService userAdvertisementService)
        {
            _advertisementService = advertisementService;
            _mapService = mapService;
            _categoryService = categoryService;
            _featureHandlerService = featureHandlerService;
            _fileCenterService = fileCenterService;
            _userAdvertisementService = userAdvertisementService;
        }

        [HttpGet("")]
        public IActionResult GetAdvertisementList([FromQuery] AdvertisemenFilterDTO filterDto,
            [FromQuery] PaginationFilterDTO pagination)
        {
            return Ok(_advertisementService.GetAdvertisementList(filterDto, pagination));
        }

        [HttpGet("{id:int}")]
        public IActionResult GetAdvertisementDetail(int id)
        {
            var userId = User.Identity?.Name;

            var advertisement = _advertisementService.GetAdvertisementDetail(id, acceptedOnly: true, userId: userId);
            if (advertisement == null)
                return NotFound();

            if (User.Identity?.IsAuthenticated == true)
            {
                _userAdvertisementService.AddAdvertisementToUserHistory(userId, id);
            }

            return Ok(advertisement);
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

            if (dto.PriceType == AdvertisementPriceTypeEnum.Price && dto.PriceValue < 0)
            {
                ModelState.AddModelError(nameof(dto.PriceValue), "مقدار وارد شده باید بزرگتر یا برابر با 0 باشد");
                hasErrors = true;
            }
            else if (dto.PriceType == AdvertisementPriceTypeEnum.Agreement)
            {
                dto.Price.Value = 0;
            }

            var pictureValidation =
                _fileCenterService.ValidateFileTypes(dto.Pictures, FileCenterTypeEnum.AdvertisementPicture);
            if (!pictureValidation.IsSuccess)
            {
                ModelState.AddModelError(nameof(dto.Pictures), pictureValidation.Message);
                hasErrors = true;
            }

            if ((dto.Latitude == null && dto.Longitude != null) || (dto.Latitude != null && dto.Longitude == null))
            {
                if (dto.Latitude == null)
                {
                    ModelState.AddModelError(nameof(dto.Latitude), "این فیلد اجباری است");
                }
                else
                {
                    ModelState.AddModelError(nameof(dto.Longitude), "این فیلد اجباری است");
                }

                hasErrors = true;
            }
            else if (dto.Latitude != null && dto.Longitude != null)
            {
                var locationValidation =
                    _mapService.ValidateLocation(dto.ProvinceId, dto.CityId, (double)dto.Longitude,
                        (double)dto.Latitude);
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
                        case LocationValidationStatusEnum.ServerError:
                            return StatusCode(500, locationValidation);
                    }

                    hasErrors = true;
                }

                if (dto.ShowExactCoordinates == null)
                {
                    ModelState.AddModelError(nameof(dto.ShowExactCoordinates), "این فیلد اجباری است");
                    hasErrors = true;
                }
            }

            dto.Features.RemoveAll(f => f.Value == null);
            var featureValidation = _featureHandlerService.ValidateAdvertisementFeatures(dto.CategoryId, dto.Features);
            if (!featureValidation.IsSuccess)
            {
                ModelState.AddModelError(nameof(dto.Features), featureValidation.Message);
                hasErrors = true;
            }

            if (hasErrors) return ValidationProblem(ModelState);

            var advertisementId = _advertisementService.CreateAdvertisement(dto, User.Identity.Name);
            return CreatedAtAction(nameof(GetAdvertisementDetail), new { Id = advertisementId }, new { Id = advertisementId });
        }

        #region MySelf

        [HttpGet("myself")]
        [Authorize]
        public IActionResult GetAdvertisementList([FromQuery] PaginationFilterDTO pagination)
        {
            var userId = User.Identity?.Name;
            return Ok(_advertisementService.GetSelfAdvertisementList(userId, pagination));
        }

        [Authorize]
        [HttpDelete("myself/{id:int}")]
        public IActionResult DeleteSelfAdvertisement(int id)
        {
            var userId = User.Identity?.Name;
            var isAdvertisementExists = _advertisementService.IsAdvertisementExists(id, userId);
            if (!isAdvertisementExists)
                return NotFound();

            var dto = new AdvertisementUpdateStatusDTO { StatusType = AdvertisementStatusTypeEnum.DeletedByUser };
            var result = _advertisementService.UpdateAdvertisementStatus(id, dto);
            return NoContent();
        }

        [Authorize]
        [HttpPut("myself/{id:int}")]
        public IActionResult UpdateSelfAdvertisement(int id, [FromBody] UpdateAdvertisementDTO dto)
        {
            var userId = User.Identity?.Name;
            var advertisement = _advertisementService.GetAdvertisement(id);
            if (advertisement == null || advertisement.UserId != userId)
                return NotFound();

            if (!ModelState.IsValid) return BadRequest(dto);

            bool hasErrors = false;

            var pictureValidation =
                _fileCenterService.ValidateFileTypes(dto.Pictures, FileCenterTypeEnum.AdvertisementPicture);
            if (!pictureValidation.IsSuccess)
            {
                ModelState.AddModelError(nameof(dto.Pictures), pictureValidation.Message);
                hasErrors = true;
            }

            if (dto.PriceType == AdvertisementPriceTypeEnum.Price && dto.PriceValue < 0)
            {
                ModelState.AddModelError(nameof(dto.PriceValue), "مقدار وارد شده باید بزرگتر یا برابر با 0 باشد");
                hasErrors = true;
            }
            else if (dto.PriceType == AdvertisementPriceTypeEnum.Agreement)
            {
                dto.Price.Value = 0;
            }

            if ((dto.Latitude == null && dto.Longitude != null) || (dto.Latitude != null && dto.Longitude == null))
            {
                if (dto.Latitude == null)
                {
                    ModelState.AddModelError(nameof(dto.Latitude), "این فیلد اجباری است");
                }
                else
                {
                    ModelState.AddModelError(nameof(dto.Longitude), "این فیلد اجباری است");
                }

                hasErrors = true;
            }
            else if (dto.Latitude != null && dto.Longitude != null)
            {
                var locationValidation =
                    _mapService.ValidateLocation(advertisement.ProvinceId, advertisement.CityId, (double)dto.Longitude,
                        (double)dto.Latitude);
                if (!locationValidation.IsValid)
                {
                    switch (locationValidation.Status)
                    {
                        case LocationValidationStatusEnum.ProvinceNotFound:
                            //ModelState.AddModelError(nameof(dto.ProvinceId), locationValidation.Message);
                            throw new ArgumentOutOfRangeException();
                        case LocationValidationStatusEnum.CityNotFound:
                            //ModelState.AddModelError(nameof(dto.CityId), locationValidation.Message);
                            throw new ArgumentOutOfRangeException();
                        case LocationValidationStatusEnum.CoordinatesNotInProvince:
                            ModelState.AddModelError(nameof(dto.Latitude), locationValidation.Message);
                            ModelState.AddModelError(nameof(dto.Longitude), locationValidation.Message);
                            break;
                        case LocationValidationStatusEnum.ServerError:
                            return StatusCode(500, locationValidation);
                    }

                    hasErrors = true;
                }

                if (dto.ShowExactCoordinates == null)
                {
                    ModelState.AddModelError(nameof(dto.ShowExactCoordinates), "این فیلد اجباری است");
                    hasErrors = true;
                }
            }

            var featureValidation =
                _featureHandlerService.ValidateAdvertisementFeatures(advertisement.CategoryId, dto.Features);
            if (!featureValidation.IsSuccess)
            {
                ModelState.AddModelError(nameof(dto.Features), featureValidation.Message);
                hasErrors = true;
            }

            if (hasErrors) return ValidationProblem(ModelState);

            var result = _advertisementService.UpdateAdvertisement(id, dto);
            return Ok(result);
        }

        [Authorize]
        [HttpPost("myself/{id:int}/pictures")]
        public IActionResult AddSelfAdvertisementPicture(int id, AdvertisementAddPicturesDTO dto)
        {
            var userId = User.Identity?.Name;
            var isAdvertisementExists = _advertisementService.IsAdvertisementExists(id, userId);
            if (!isAdvertisementExists)
                return NotFound();

            if (!ModelState.IsValid) return BadRequest(dto);

            var pictureValidation =
                _fileCenterService.ValidateFileTypes(dto.Pictures, FileCenterTypeEnum.AdvertisementPicture);
            if (!pictureValidation.IsSuccess)
            {
                ModelState.AddModelError(nameof(dto.Pictures), pictureValidation.Message);
                return ValidationProblem(ModelState);
            }

            var updateDto = new UpdateAdvertisementPictureDTO
            {
                Pictures = dto.Pictures,
                Status = UpdateAdvertisementPictureStatus.InsertPictures
            };
            _advertisementService.UpdateAdvertisementPictures(id, updateDto);

            return StatusCode(201);
        }

        [Authorize]
        [HttpDelete("myself/{id:int}/pictures")]
        public IActionResult DeleteSelfAdvertisementPicture(int id, AdvertisementDeletePicturesDTO dto)
        {
            var userId = User.Identity?.Name;
            var isAdvertisementExists = _advertisementService.IsAdvertisementExists(id, userId);
            if (!isAdvertisementExists)
                return NotFound();

            if (!ModelState.IsValid) return BadRequest(dto);

            var updateDto = new UpdateAdvertisementPictureDTO
            {
                Pictures = dto.Pictures,
                Status = UpdateAdvertisementPictureStatus.DeletePictures
            };
            _advertisementService.UpdateAdvertisementPictures(id, updateDto);

            return StatusCode(204);
        }

        [Authorize]
        [HttpGet("myself/{id:int}/features")]
        public IActionResult GetAdvertisementFeatureDetail(int id)
        {
            var userId = User.Identity?.Name;

            var isAdvertisementExists = _advertisementService.IsAdvertisementExists(id, userId);
            if (!isAdvertisementExists)
                return NotFound();



            return Ok(_featureHandlerService.GetAdvertisementFeaturesList(id));
        }
        #endregion

        #region Actions

        [Authorize]
        [HttpPost("{id:int}/bookmark")]
        public IActionResult AddAdvertisementBookmark(int id, [FromBody] AdvertisementBookmarkDTO dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(dto);

            var isAdvertisementExists = _advertisementService.IsAdvertisementExists(id);
            if (!isAdvertisementExists)
                return NotFound();

            var userId = User.Identity.Name;
            var result = _userAdvertisementService.AddOrRemoveBookmark(dto, userId, id);
            return Ok(result);
        }

        [Authorize]
        [HttpDelete("{id:int}/note")]
        public IActionResult RemoveAdvertisementNote(int id)
        {
            var isAdvertisementExists = _advertisementService.IsAdvertisementExists(id);
            if (!isAdvertisementExists)
                return NotFound();

            var userId = User.Identity.Name;
            var result = _userAdvertisementService.RemoveNote(userId, id);
            if (result)
            {
                return Ok(new OperationResultDTO
                {
                    IsSuccess = true
                });
            }

            return StatusCode(500, new OperationResultDTO
            {
                IsSuccess = false,
                Message = "خطا در حذف یادداشت"
            });
        }

        [Authorize]
        [HttpGet("{id:int}/contact")]
        public IActionResult GetAdvertisementContactDetail(int id)
        {
            var advertisement = _advertisementService.GetAdvertisement(id);
            if (advertisement == null)
                return NotFound();

            if (advertisement.ContactType is AdvertisementContactTypeEnum.ChatOnly)
                return Ok(new UserContactViewModel
                {
                    Success = false,
                    ErrorMessage = "اطلاعات تماس کاربر مخفی میباشد. از چت استفاده کنید"
                });

            var userId = advertisement.UserId;

            var result = _userAdvertisementService.GetUserContactDetail(userId);
            if (result.Success)
                return Ok(result);

            return StatusCode(403, result);
        }

        [Authorize]
        [HttpPost("{id:int}/note")]
        public IActionResult AddAdvertisementNote(int id, [FromBody] CreateAdvertisementNoteDTO dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(dto);

            var isAdvertisementExists = _advertisementService.IsAdvertisementExists(id);
            if (!isAdvertisementExists)
                return NotFound();

            var userId = User.Identity.Name;
            var addNoteResult = _userAdvertisementService.AddNote(dto, userId, id);
            if (addNoteResult)
            {
                return Ok(new OperationResultDTO
                {
                    IsSuccess = true
                });
            }

            return StatusCode(500, new OperationResultDTO
            {
                IsSuccess = false,
                Message = "خطا در ثبت یادداشت"
            });
        }

        #endregion
    }
}