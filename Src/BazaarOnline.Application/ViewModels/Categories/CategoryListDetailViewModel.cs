namespace BazaarOnline.Application.ViewModels.Categories
{
    public class CategoryListDetailViewModel
    {
        public int Id { get; set; }

        public int? ParentId { get; set; }

        public string Icon { get; set; } = string.Empty;

        public string Title { get; set; } = string.Empty;

        public bool HasChildren { get; set; } = false;

        public int IndentLevel { get; set; } = 0;

        public IList<CategoryListDetailViewModel> Children { get; set; } =
            new List<CategoryListDetailViewModel>();
    }
}