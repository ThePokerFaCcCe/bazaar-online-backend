using BazaarOnline.Domain.Entities.UploadCenter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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