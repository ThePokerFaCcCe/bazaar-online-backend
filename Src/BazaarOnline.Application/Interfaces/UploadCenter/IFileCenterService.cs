using BazaarOnline.Application.DTOs;
using BazaarOnline.Domain.Entities.UploadCenter;
using Microsoft.AspNetCore.Http;

namespace BazaarOnline.Application.Interfaces.UploadCenter;

public interface IFileCenterService
{
    OperationResultDTO Validate(IFormFile file, FileCenterTypeEnum type);
    OperationResultDTO Validate(List<IFormFile> files, FileCenterTypeEnum type);

    /// <summary>
    /// Save an file and create thumbnail for that
    /// </summary>
    /// <param name="image"></param>
    /// <returns>Created Model</returns>
    FileCenter SaveImage(IFormFile image, FileCenterTypeEnum type);

    /// <summary>
    /// Save a file
    /// </summary>
    /// <param name="filee"></param>
    /// <returns>Created Model</returns>
    FileCenter SaveFile(IFormFile file, FileCenterTypeEnum type);

    /// <summary>
    /// Save images and create thumbnail for them
    /// </summary>
    /// <param name="images"></param>
    /// <returns>Created Models</returns>
    List<FileCenter> SaveImage(List<IFormFile> images, FileCenterTypeEnum type);

    /// <summary>
    /// Validate that all files are in same type
    /// </summary>
    /// <param name="fileIds"></param>
    /// <param name="type"></param>
    /// <returns></returns>
    OperationResultDTO ValidateFileTypes(List<int> fileIds, FileCenterTypeEnum type);
}