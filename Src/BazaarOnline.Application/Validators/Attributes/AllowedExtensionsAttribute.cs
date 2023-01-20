using System.Collections;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace BazaarOnline.Application.Validators.Attributes;

public class AllowedExtensionsAttribute : ValidationAttribute
{
    private readonly string[] _extensions;

    public AllowedExtensionsAttribute(string[] extensions)
    {
        _extensions = extensions;
    }

    protected override ValidationResult IsValid(
        object value, ValidationContext validationContext)
    {
        if (value is IFormFile file)
        {
            var extension = Path.GetExtension(file.FileName);
            if (!((IList)_extensions).Contains(extension.Trim().ToLower()))
            {
                return new ValidationResult(GetErrorMessage());
            }
        }

        return ValidationResult.Success;
    }

    public string GetErrorMessage()
    {
        return $"پسوند فایل مجاز نمی باشد.";
    }
}