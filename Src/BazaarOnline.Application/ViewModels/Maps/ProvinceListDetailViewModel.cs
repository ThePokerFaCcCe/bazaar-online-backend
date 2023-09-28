namespace BazaarOnline.Application.ViewModels.Maps
{
    public class ProvinceListDetailViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int CitiesCount { get; set; } = 0;

        public IEnumerable<CityListDetailViewModel> Cities { get; set; }
    }
}