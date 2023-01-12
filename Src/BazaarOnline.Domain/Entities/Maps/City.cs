namespace BazaarOnline.Domain.Entities.Maps
{
    public class City
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string AmarCode { get; set; }

        public int ProvinceId { get; set; }

        #region Relations

        public Province Province { get; set; }

        #endregion
    }
}