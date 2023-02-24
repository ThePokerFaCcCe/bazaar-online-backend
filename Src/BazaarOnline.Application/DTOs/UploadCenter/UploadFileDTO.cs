using System.ComponentModel.DataAnnotations;
using BazaarOnline.Domain.Entities.UploadCenter;
using Microsoft.AspNetCore.Http;

namespace BazaarOnline.Application.DTOs.UploadCenter;

public class UploadFileDTO
{
    [Required] public FileCenterTypeEnum Type { get; set; }

    [Required] public IFormFile File { get; set; }
}