using BazaarOnline.Application.ViewModels.Categories;
using BazaarOnline.Application.ViewModels.Categories.CategoryFeatures;

namespace BazaarOnline.Application.Interfaces.Categories
{
    public interface ICategoryService
    {
        IEnumerable<CategoryListDetailViewModel> GetAllCategories();

        CategoryTreeNodeTypeEnum? GetCategoryType(int categoryId);

        bool IsCategoryExists(int id);
    }
}