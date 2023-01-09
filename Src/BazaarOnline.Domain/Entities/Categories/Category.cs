namespace BazaarOnline.Domain.Entities.Categories
{
    public class Category
    {
        public int Id { get; set; }
        
        public string Title { get; set; }

        public int? ParentCategoryId { get; set; }

        #region Relations

        public Category? ParentCategory { get; set; }

        public IList<Category> ChildCategories { get; set; }

        #endregion

    }
}
