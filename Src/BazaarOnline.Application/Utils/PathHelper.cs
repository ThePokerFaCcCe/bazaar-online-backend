namespace BazaarOnline.Application.Utils
{
    public class PathHelper
    {
        public static readonly string wwwroot = Directory.GetCurrentDirectory() + "/wwwroot/";

        #region Advertiesements

        private static readonly string AdvertisementsBase = "advertiesements/";
        public static readonly string AdvertisementImages = AdvertisementsBase + "images/";
        public static readonly string AdvertisementThumbs = AdvertisementsBase + "thumbnails/";

        #endregion

        public static readonly string OtherFiles = "other";
    }
}