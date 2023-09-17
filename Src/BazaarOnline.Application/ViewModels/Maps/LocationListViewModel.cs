using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BazaarOnline.Application.Utils.Extensions;

namespace BazaarOnline.Application.ViewModels.Maps
{
    public class LocationListViewModel
    {
        public int Id { get; set; }
        public int ProvinceId { get; set; }

        public string Name { get; set; }

        public LocationTypeEnum LocationTypeId { get; set; }

        public string LocationTypeName => LocationTypeId.GetDisplayName() ?? string.Empty;
    }
}