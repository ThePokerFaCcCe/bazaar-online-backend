using BazaarOnline.Application.DTOs;
using BazaarOnline.Domain.Entities.UploadCenter;
using Microsoft.AspNetCore.Http;

namespace BazaarOnline.Application.Interfaces.UploadCenter;

public interface IFileCenterService
{
    OperationResultDTO Validate(IFormFile file, FileCenterTypeEnum type);

    /// <summary>
    /// Save an image and create thumbnail for that
    /// </summary>
    /// <param name="image"></param>
    /// <param name="imagePath">path without 'wwwroot'</param>
    /// <param name="thumbPath">path without 'wwwroot'</param>
    /// <returns>Image filename</returns>
    FileCenter SaveImage(IFormFile image, FileCenterTypeEnum type);

    /// <summary>
    /// Validate that all files are in same type
    /// </summary>
    /// <param name="fileIds"></param>
    /// <param name="type"></param>
    /// <returns></returns>
    OperationResultDTO ValidateFileTypes(List<int> fileIds, FileCenterTypeEnum type);
}