using BazaarOnline.Application.ViewModels.Categories;

namespace BazaarOnline.Application.Interfaces.Categories
{
    public interface ICategoryService
    {
        IEnumerable<CategoryListDetailViewModel> GetAllCategories();

        CategoryTreeNodeTypeEnum? GetCategoryType(int categoryId);

        #region Find

        bool IsCategoryExists(int id);

        #endregion
    }
}