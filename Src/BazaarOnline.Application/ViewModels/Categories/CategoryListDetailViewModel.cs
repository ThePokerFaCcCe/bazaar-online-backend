namespace BazaarOnline.Application.ViewModels.Categories
{
    public class CategoryListDetailViewModel
    {
        public int Id { get; set; }

        public int? ParentId { get; set; }

        public string Title { get; set; }

        public bool HasChildren { get; set; } = false;

        public IList<CategoryListDetailViewModel> Children { get; set; } = new List<CategoryListDetailViewModel>();
    }
}
