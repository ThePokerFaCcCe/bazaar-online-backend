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

        public string PictureName { get; set; }

        public int AdvertisementId { get; set; }

        #region Relations

        public Advertisement Advertisement { get; set; }

        #endregion
    }
}