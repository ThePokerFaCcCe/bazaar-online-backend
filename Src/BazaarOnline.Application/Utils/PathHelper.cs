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

        #region Chats

        private static readonly string ChatsBase = "chats/";
        public static readonly string ChatImages = ChatsBase + "images/";
        public static readonly string ChatThumbs = ChatsBase + "thumbnails/";
        public static readonly string ChatVoice = ChatsBase + "voices/";

        #endregion

        public static readonly string OtherFiles = "other";
    }
}