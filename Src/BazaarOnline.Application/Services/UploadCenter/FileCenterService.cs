using BazaarOnline.Application.DTOs;
using BazaarOnline.Application.Interfaces.UploadCenter;
using BazaarOnline.Application.Utils;
using BazaarOnline.Application.Validators;
using BazaarOnline.Domain.Entities.UploadCenter;
using BazaarOnline.Domain.Interfaces;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace BazaarOnline.Application.Services.UploadCenter;

public class FileCenterService : IFileCenterService
{
    private readonly IRepository _repository;

    public FileCenterService(IRepository repository)
    {
        _repository = repository;
    }

    public OperationResultDTO Validate(IFormFile file, FileCenterTypeEnum type)
    {
        switch (type)
        {
            case FileCenterTypeEnum.AdvertisementPicture:
                return ValidateImage(file, 2048);

            default:
                throw new ArgumentException("This type isn't implemented!");
        }
    }

    private OperationResultDTO ValidateImage(IFormFile file, int fileSizeKb)
    {
        var allowedExtensions = new[] { ".jpg", ".png" };
        var errors = new List<string>();

        if (!file.HasValidExtension(allowedExtensions))
        {
            errors.Add("فرمت فایل مجاز نیست. باید jpg یا png باشد");
        }

        if (!file.IsValidImage())
        {
            errors.Add("فایل انتخاب شده عکس نیست");
        }

        else if (!file.IsSizeSmallerThan(fileSizeKb))
        {
            errors.Add($"حجم فایل بزرگتر از {fileSizeKb} کیلوبایت است.");
        }

        if (errors.Any())
        {
            return new OperationResultDTO
            {
                Message = JsonConvert.SerializeObject(errors),
                IsSuccess = false,
            };
        }

        return new OperationResultDTO
        {
            IsSuccess = true,
        };
    }

    public FileCenter SaveImage(IFormFile image, FileCenterTypeEnum type)
    {
        string imagePath = PathHelper.OtherFiles;
        string thumbPath = PathHelper.OtherFiles;

        switch (type)
        {
            case FileCenterTypeEnum.AdvertisementPicture:
                imagePath = PathHelper.AdvertisementImages;
                thumbPath = PathHelper.AdvertisementThumbs;
                break;
        }

        var filename = FileHelper.SaveImageWithThumb(image, imagePath, thumbPath);
        try
        {
            var file = new FileCenter
            {
                FileName = filename,
                FileType = Path.GetExtension(image.FileName),
                SizeKB = image.Length / 1024,
                UsageType = type,
            };

            _repository.Add(file);
            _repository.Save();
            return file;
        }
        catch (Exception e)
        {
            FileHelper.DeleteFile(Path.Combine(imagePath, filename));
            FileHelper.DeleteFile(Path.Combine(thumbPath, filename));
            throw;
        }
    }

    public OperationResultDTO ValidateFileTypes(List<int> fileIds, FileCenterTypeEnum type)
    {
        var files= _repository.GetAll<FileCenter>()
            .Where(fc=>fileIds.Contains(fc.Id))
            .Select(fc => new { fc.Id,fc.UsageType});

        var wrongTypeFiles = files.Where(fc => fc.UsageType != type).ToList();

        var notExists = fileIds.Except(files.Select(f => f.Id)).ToList();

        var errors = new Dictionary<int, string>();

        wrongTypeFiles.ForEach(f => errors[f.Id] = "Selected file have different type");
        notExists.ForEach(id => errors[id] = "File not found");

        if (errors.Any())
            return new OperationResultDTO
            {
                IsSuccess = false,
                Message = Newtonsoft.Json.JsonConvert.SerializeObject(errors),
            };
        

        return new OperationResultDTO { IsSuccess = true };
    } 
}