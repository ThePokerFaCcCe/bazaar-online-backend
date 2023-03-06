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

    private readonly int advertisementImageSize = 5126;

    public OperationResultDTO Validate(IFormFile file, FileCenterTypeEnum type)
    {
        switch (type)
        {
            case FileCenterTypeEnum.AdvertisementPicture:
                return ValidateImage(file, advertisementImageSize);

            default:
                throw new ArgumentException("This type isn't implemented!");
        }
    }

    public OperationResultDTO Validate(List<IFormFile> files, FileCenterTypeEnum type)
    {
        if (type is FileCenterTypeEnum.AdvertisementPicture)
        {
            var errors = new Dictionary<int, string>();
            for (int i = 0; i < files.Count; i++)
            {
                var validation = ValidateImage(files[i], advertisementImageSize);
                if (!validation.IsSuccess)
                    errors[i] = validation.Message;
            }

            if (errors.Any())
            {
                return new OperationResultDTO
                {
                    Message = Newtonsoft.Json.JsonConvert.SerializeObject(errors)
                };
            }

            return new OperationResultDTO { IsSuccess = true };
        }

        throw new ArgumentException("This type isn't implemented!");
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

    public List<FileCenter> SaveImage(List<IFormFile> images, FileCenterTypeEnum type)
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

        var createdFiles = new List<FileCenter>();

        foreach (var image in images)
        {
            var filename = FileHelper.SaveImageWithThumb(image, imagePath, thumbPath);

            var file = new FileCenter
            {
                FileName = filename,
                FileType = Path.GetExtension(image.FileName),
                SizeKB = image.Length / 1024,
                UsageType = type,
            };

            createdFiles.Add(file);
        }

        try
        {
            _repository.AddRange(createdFiles);
            _repository.Save();
        }
        catch (Exception e)
        {
            createdFiles.ForEach(f =>
            {
                FileHelper.DeleteFile(Path.Combine(imagePath, f.FileName));
                FileHelper.DeleteFile(Path.Combine(thumbPath, f.FileName));
            });
            throw;
        }

        return createdFiles;
    }


    public OperationResultDTO ValidateFileTypes(List<int> fileIds, FileCenterTypeEnum type)
    {
        return new OperationResultDTO();
    }
}