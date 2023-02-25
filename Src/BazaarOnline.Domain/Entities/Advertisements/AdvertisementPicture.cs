using BazaarOnline.Domain.Entities.UploadCenter;

namespace BazaarOnline.Domain.Entities.Advertisements
{
    public class AdvertisementPicture
    {
        public int Id { get; set; }

        public int AdvertisementId { get; set; }

        public int FileCenterId { get; set; }

        #region Relations

        public Advertisement Advertisement { get; set; }

        public FileCenter FileCenter { get; set; }

        #endregion
    }
}