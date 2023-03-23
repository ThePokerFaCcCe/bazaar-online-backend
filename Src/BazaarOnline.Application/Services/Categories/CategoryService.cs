using BazaarOnline.Application.Interfaces.Categories;
using BazaarOnline.Application.ViewModels.Categories;
using BazaarOnline.Domain.Entities.Categories;
using BazaarOnline.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BazaarOnline.Application.Services.Categories
{
    public class CategoryService : ICategoryService
    {
        private readonly IRepository _repository;

        public CategoryService(IRepository repository)
        {
            _repository = repository;
        }

        private void SetLevel(IEnumerable<CategoryListDetailViewModel> categories, int level)
        {
            foreach (var c in categories)
            {
                c.IndentLevel = level;
                SetLevel(c.Children, level + 1);
            }
        }

        public IEnumerable<CategoryListDetailViewModel> GetAllCategories()
        {
            var categories = _repository.GetAll<Category>()
                .Select(c => new CategoryListDetailViewModel()
                {
                    Id = c.Id,
                    ParentId = c.ParentCategoryId,
                    Title = c.Title,
                    HasChildren = false,
                    Children = new List<CategoryListDetailViewModel>(),
                }).ToList();

            categories.ForEach(cParent =>
            {
                categories.ForEach(cChild =>
                {
                    if (cChild.ParentId == cParent.Id)
                    {
                        cParent.Children.Add(cChild);
                        cParent.HasChildren = true;
                    }
                });
            });
            categories = categories.Where(c => c.ParentId == null).ToList();
            SetLevel(categories, 0);
            return categories;
        }

        public CategoryTreeNodeTypeEnum? GetCategoryType(int categoryId)
        {
            var category = _repository.GetAll<Category>()
                .Include(c => c.ChildCategories)
                .SingleOrDefault(c => c.Id == categoryId);

            if (category == null) return null;

            bool hasChildren = category.ChildCategories.Any();
            bool hasParent = (category.ParentCategory == null);

            if (hasParent && hasChildren)
                return CategoryTreeNodeTypeEnum.Internal;

            if (!hasParent && hasChildren)
                return CategoryTreeNodeTypeEnum.Root;

            return CategoryTreeNodeTypeEnum.Leaf;
        }

        public bool IsCategoryExists(int id)
        {
            return _repository.GetAll<Category>()
                .Any(c => c.Id == id);
        }
    }
}