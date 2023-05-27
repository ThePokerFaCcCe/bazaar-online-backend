using BazaarOnline.Application.DTOs;
using BazaarOnline.Application.Generators;
using BazaarOnline.Application.Interfaces.UploadCenter;
using BazaarOnline.Application.Utils;
using BazaarOnline.Application.Validators;
using BazaarOnline.Domain.Entities.UploadCenter;
using BazaarOnline.Domain.Interfaces;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using NVorbis;

namespace BazaarOnline.Application.Services.UploadCenter;

public class FileCenterService : IFileCenterService
{
    private readonly IRepository _repository;

    public FileCenterService(IRepository repository)
    {
        _repository = repository;
    }

    private const int AdvertisementImageSize = 5126;
    private const int ChatVoiceSize = 20480;
    private const int ChatImageSize = 10240;

    public OperationResultDTO Validate(IFormFile file, FileCenterTypeEnum type)
    {
        switch (type)
        {
            case FileCenterTypeEnum.AdvertisementPicture:
                return ValidateImage(file, AdvertisementImageSize);
            case FileCenterTypeEnum.ChatVoice:
                return ValidateVoice(file, ChatVoiceSize);
            case FileCenterTypeEnum.ChatPicture:
                return ValidateImage(file, ChatImageSize);

            default:
                throw new ArgumentOutOfRangeException(nameof(type));
        }
    }

    public OperationResultDTO Validate(List<IFormFile> files, FileCenterTypeEnum type)
    {
        var errors = new Dictionary<int, string>();
        for (int i = 0; i < files.Count; i++)
        {
            IFormFile file = files[i];
            var validation = new OperationResultDTO();

            switch (type)
            {
                case FileCenterTypeEnum.AdvertisementPicture:
                    validation = ValidateImage(file, AdvertisementImageSize);
                    break;
                case FileCenterTypeEnum.ChatVoice:
                    validation = ValidateVoice(file, ChatVoiceSize);
                    break;
                case FileCenterTypeEnum.ChatPicture:
                    validation = ValidateImage(file, ChatImageSize);
                    break;

                default:
                    throw new ArgumentOutOfRangeException(nameof(type));
            }

            if (!validation.IsSuccess)
            {
                errors[i] = validation.Message;
            }
        }

        if (errors.Any())
        {
            return new OperationResultDTO
            {
                Message = JsonConvert.SerializeObject(errors)
            };
        }

        return new OperationResultDTO { IsSuccess = true };
    }


    private OperationResultDTO ValidateImage(IFormFile file, int fileSizeKb)
    {
        var allowedExtensions = new[] { ".jpg", ".png", ".jpeg" };
        var errors = new List<string>();

        if (!file.HasValidExtension(allowedExtensions))
        {
            errors.Add("فرمت فایل مجاز نیست. باید jpg یا png یا jpeg باشد");
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

    private OperationResultDTO ValidateVoice(IFormFile file, int fileSizeKb)
    {
        var errors = new List<string>();
        // TODO - FixLater
        //if (!file.IsValidOggVoice())
        //{
        //    errors.Add("فایل انتخاب شده وویس نیست");
        //}

        if (!file.IsSizeSmallerThan(fileSizeKb))
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
            case FileCenterTypeEnum.ChatPicture:
                imagePath = PathHelper.ChatImages;
                thumbPath = PathHelper.ChatThumbs;
                break;


            default:
                throw new ArgumentOutOfRangeException(nameof(type));
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

    public FileCenter SaveFile(IFormFile file, FileCenterTypeEnum type)
    {
        string filePath = PathHelper.OtherFiles;
        string? fileName = null;

        switch (type)
        {
            case FileCenterTypeEnum.ChatVoice:
                filePath = PathHelper.ChatVoice;
                fileName = $"{StringGenerator.GenerateUniqueCodeWithoutDash()}.ogg";
                break;

            default:
                throw new ArgumentOutOfRangeException(nameof(type));
        }

        var savedFilePath = FileHelper.SaveFile(file, filePath, fileName);
        var filename = Path.GetFileName(savedFilePath);
        try
        {
            object? extra = null;
            if (type is FileCenterTypeEnum.ChatVoice)
            {
                using var f = new VorbisReader(file.OpenReadStream(), false);
                extra = new
                {
                    totalTime = (long)f.TotalTime.TotalSeconds,
                };
            }

            var fileCenter = new FileCenter
            {
                FileName = filename,
                FileType = Path.GetExtension(file.FileName),
                SizeKB = file.Length / 1024,
                UsageType = type,
                ExtraProperties = (extra == null) ? null : JsonConvert.SerializeObject(extra),
            };

            _repository.Add(fileCenter);
            _repository.Save();
            return fileCenter;
        }
        catch (Exception e)
        {
            FileHelper.DeleteFile(Path.Combine(filePath, filename));
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
            case FileCenterTypeEnum.ChatPicture:
                imagePath = PathHelper.ChatImages;
                thumbPath = PathHelper.ChatThumbs;
                break;


            default:
                throw new ArgumentOutOfRangeException(nameof(type));
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
        var files = _repository.GetAll<FileCenter>()
            .Where(fc => fileIds.Contains(fc.Id))
            .Select(fc => new { fc.Id, fc.UsageType });

        var wrongTypeFiles = files.Where(fc => fc.UsageType != type).ToList();

        var notExists = fileIds.Except(files.Select(f => f.Id)).ToList();

        var errors = new Dictionary<int, string>();

        wrongTypeFiles.ForEach(f => errors[f.Id] = "Selected file have different type");
        notExists.ForEach(id => errors[id] = "File not found");

        if (errors.Any())
        {
            return new OperationResultDTO
            {
                IsSuccess = false,
                Message = Newtonsoft.Json.JsonConvert.SerializeObject(errors),
            };
        }

        return new OperationResultDTO { IsSuccess = true };
    }

    public List<FileCenter> SaveFile(List<IFormFile> files, FileCenterTypeEnum type)
    {
        string filePath = PathHelper.OtherFiles;

        switch (type)
        {
            case FileCenterTypeEnum.ChatVoice:
                filePath = PathHelper.ChatVoice;
                break;

            default:
                throw new ArgumentOutOfRangeException(nameof(type));
        }

        var createdFiles = new List<FileCenter>();

        foreach (var file in files)
        {
            string? fileName = null;
            object? extra = null;
            if (type is FileCenterTypeEnum.ChatVoice)
            {
                fileName = $"{StringGenerator.GenerateUniqueCodeWithoutDash()}.ogg";
                //using var f = new VorbisReader(file.OpenReadStream(), false);
                extra = new
                {
                    //totalTime = (long)f.TotalTime.TotalSeconds,
                    totalTime = -1,
                };
            }

            var savedFilePath = FileHelper.SaveFile(file, filePath, fileName);
            var filename = Path.GetFileName(savedFilePath);

            var fileCenter = new FileCenter
            {
                FileName = filename,
                FileType = Path.GetExtension(file.FileName).ToLower(),
                SizeKB = file.Length / 1024,
                UsageType = type,
                ExtraProperties = (extra == null) ? null : JsonConvert.SerializeObject(extra),
            };

            createdFiles.Add(fileCenter);
        }

        try
        {
            _repository.AddRange(createdFiles);
            _repository.Save();
        }
        catch (Exception e)
        {
            createdFiles.ForEach(f => { FileHelper.DeleteFile(Path.Combine(filePath, f.FileName)); });
            throw;
        }

        return createdFiles;
    }
}