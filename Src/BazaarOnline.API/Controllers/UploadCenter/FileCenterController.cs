using BazaarOnline.Application.DTOs.UploadCenter;
using BazaarOnline.Application.Interfaces.UploadCenter;
using BazaarOnline.Application.Utils.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
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
            if (!ModelState.IsValid) return BadRequest(dto);

            var validateFileResult = _fileCenterService.Validate(dto.Files, dto.Type);
            if (!validateFileResult.IsSuccess)
            {
                ModelState.AddModelError(nameof(dto.Files), validateFileResult.Message);
                return ValidationProblem(ModelState);
            }

            var files = _fileCenterService.SaveImage(dto.Files, dto.Type);

            var result = files.Select(f => new UploadFileResultDTO
            {
                Id = f.Id,
                SizeKB = f.SizeKB,
            });


            return Ok(result);
        }
    }
}