using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace BazaarOnline.Application.Validators.Attributes;

public class ValidateImageAttribute : ValidationAttribute
{
    private static readonly int maxSizeKb = 2000;
    private static readonly string[] allowedExtenstions = new[] { ".jpg", ".png" };

    private static readonly string maxSizeError = $"حجم فایل بزرگتر از {maxSizeKb} کیلوبایت است.";
    private static readonly string extensionError = "فرمت فایل مجاز نیست. باید jpg یا png باشد";
    private static readonly string badImageError = "فایل انتخاب شده عکس نیست";

    /// <summary>
    /// validate image or list of images with type of IFormFile
    /// </summary>
    public ValidateImageAttribute()
    {
    }

    private string? Validate(IFormFile file)
    {
        if (!file.HasValidExtension(allowedExtenstions))
        {
            return extensionError;
        }

        if (!file.IsSizeSmallerThan(maxSizeKb))
        {
            return maxSizeError;
        }

        if (!file.IsValidImage())
        {
            return badImageError;
        }

        return null;
    }

    protected override ValidationResult IsValid(
        object value, ValidationContext validationContext)
    {
        if (value is IFormFile file)
        {
            var result = Validate(file);
            if (result != null)
            {
                return new ValidationResult(result);
            }
        }

        else if (value is IList<IFormFile> files)
        {
            var errors = new Dictionary<int, string>();
            for (int i = 0; i < files.Count(); i++)
            {
                var result = Validate(files[i]);
                if (result != null)
                {
                    errors[i] = result;
                }
            }

            if (errors.Count > 0)
            {
                return new ValidationResult(JsonConvert.SerializeObject(errors));
            }
        }

        return ValidationResult.Success;
    }
}