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
                        ;
                    }
                });
            });

            return categories.Where(c => c.ParentId == null);
        }

        public bool IsCategoryExists(int id)
        {
            return _repository.GetAll<Category>()
                .Any(c => c.Id == id);
        }
    }
}