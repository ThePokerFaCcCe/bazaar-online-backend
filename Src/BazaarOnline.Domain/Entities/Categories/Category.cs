using BazaarOnline.Domain.Entities.Advertisements;

namespace BazaarOnline.Domain.Entities.Categories
{
    public class Category
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public int? ParentCategoryId { get; set; }

        #region Relations

        public Category? ParentCategory { get; set; }

        public IEnumerable<Category> ChildCategories { get; set; }

        public IEnumerable<Advertisement> Advertisements { get; set; }

        #endregion
    }
}