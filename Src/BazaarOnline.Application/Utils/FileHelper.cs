using BazaarOnline.Application.Converters;
using BazaarOnline.Application.Generators;
using Microsoft.AspNetCore.Http;

namespace BazaarOnline.Application.Utils
{
    public class FileHelper
    {
        private static readonly string wwwroot = Directory.GetCurrentDirectory() + "/wwwroot/";

        /// <summary>
        /// Save an image and create thumbnail for that
        /// </summary>
        /// <param name="image"></param>
        /// <param name="imagePath">path without 'wwwroot'</param>
        /// <param name="thumbPath">path without 'wwwroot'</param>
        /// <returns>Image filename</returns>
        public static string SaveImageWithThumb(IFormFile image, string imagePath, string thumbPath)
        {
            string path = FileHelper.SaveFile(image, imagePath);
            string fileName = Path.GetFileName(path);

            thumbPath = Path.Combine(wwwroot, thumbPath);
            Directory.CreateDirectory(thumbPath);

            var thumb = ImageConvertor.GetImageThumbnail(image.OpenReadStream());
            thumb.Save(Path.Combine(thumbPath, fileName));

            return fileName;
        }

        public static string SaveFile(Stream file, string filePath, string fileName)
        {
            if (!filePath.StartsWith(wwwroot))
            {
                filePath = Path.Combine(wwwroot, filePath);
            }

            Directory.CreateDirectory(filePath);
            filePath = Path.Combine(filePath, fileName);
            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                file.CopyTo(stream);
            }

            return filePath;
        }

        public static string SaveFile(IFormFile formFile, string filePath, string? fileName = null)
        {
            if (fileName == null)
            {
                fileName = StringGenerator.GenerateUniqueCodeWithoutDash() + Path.GetExtension(formFile.FileName);
            }

            filePath = SaveFile(formFile.OpenReadStream(), filePath, fileName);

            return filePath;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="path">path without 'wwwroot'</param>
        public static void DeleteFile(string path)
        {
            path = Path.Combine(wwwroot, path);
            if (File.Exists(path))
            {
                File.Delete(path);
            }
        }
    }
}