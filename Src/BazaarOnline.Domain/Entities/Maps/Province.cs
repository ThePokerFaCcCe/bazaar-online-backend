namespace BazaarOnline.Domain.Entities.Maps
{
    public class Province
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string AmarCode { get; set; }

        #region Relations

        public IEnumerable<City> Cities { get; set; }

        #endregion
    }
}