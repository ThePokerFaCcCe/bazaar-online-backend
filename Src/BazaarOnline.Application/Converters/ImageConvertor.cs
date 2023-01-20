using System.Drawing;

namespace BazaarOnline.Application.Converters
{
    public class ImageConvertor
    {
        public static Image GetImageThumbnail(Stream resourceImage, int width = 250, int height = 250)
        {
            var image = Image.FromStream(resourceImage);
            var thumb = image.GetThumbnailImage(width, height, () => false, IntPtr.Zero);

            return thumb;
        }
    }
}