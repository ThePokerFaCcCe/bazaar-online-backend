using BazaarOnline.Application.DTOs;
using BazaarOnline.Application.DTOs.UploadCenter;
using BazaarOnline.Application.Interfaces.UploadCenter;
using BazaarOnline.Application.Utils.Extensions;
using BazaarOnline.Domain.Entities.UploadCenter;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BazaarOnline.API.Controllers.UploadCenter
{
    [Route("api/v1/uploads")]
    [ApiController]
    [Authorize]
    public class FileCenterController : ControllerBase
    {
        private readonly IFileCenterService _fileCenterService;

        public FileCenterController(IFileCenterService fileCenterService)
        {
            _fileCenterService = fileCenterService;
        }

        [HttpPost("images")]
        public ActionResult<IEnumerable<UploadFileResultDTO>> UploadImage([FromForm] UploadFileDTO dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(dto);
            }

            var validateFileResult = _fileCenterService.Validate(dto.Files, dto.Type);
            if (!validateFileResult.IsSuccess)
            {
                ModelState.AddModelError(nameof(dto.Files), validateFileResult.Message);
                return ValidationProblem(ModelState);
            }

            var files = _fileCenterService.SaveImage(dto.Files, dto.Type);

            var result = files.Select(f => new UploadFileResultDTO().FillFromObject(f));


            return Ok(result);
        }

        [HttpPost("files")]
        public ActionResult<IEnumerable<UploadFileResultDTO>> UploadFile([FromForm] UploadFileDTO dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(dto);
            }

            var validateFileResult = _fileCenterService.Validate(dto.Files, dto.Type);
            if (!validateFileResult.IsSuccess)
            {
                ModelState.AddModelError(nameof(dto.Files), validateFileResult.Message);
                return ValidationProblem(ModelState);
            }

            var files = new List<FileCenter>();
            switch (dto.Type)
            {
                case FileCenterTypeEnum.AdvertisementPicture:
                    files = _fileCenterService.SaveImage(dto.Files, dto.Type);
                    break;
                case FileCenterTypeEnum.ChatPicture:
                    files = _fileCenterService.SaveImage(dto.Files, dto.Type);
                    break;
                case FileCenterTypeEnum.ChatVoice:
                    files = _fileCenterService.SaveFile(dto.Files, dto.Type);
                    break;
                default:
                    ModelState.AddModelError(nameof(dto.Type), "Type isn't valid");
                    return ValidationProblem(ModelState);
            }


            var result = files.Select(f => new UploadFileResultDTO().FillFromObject(f));


            return Ok(new OperationResultDTO
            {
                IsSuccess = true,
                Data = result,
            });
        }
    }
}