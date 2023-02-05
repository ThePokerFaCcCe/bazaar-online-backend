using BazaarOnline.Application.Interfaces.Categories;
using BazaarOnline.Application.Interfaces.Features;
using BazaarOnline.Application.ViewModels.Categories;
using BazaarOnline.Application.ViewModels.Categories.CategoryFeatures;
using Microsoft.AspNetCore.Mvc;

namespace BazaarOnline.API.Controllers.Categories
{
    [Route("api/v1/categories")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _categoryService;
        private readonly IFeatureHandlerService _featureHandlerService;

        public CategoryController(ICategoryService categoryService, IFeatureHandlerService featureHandlerService)
        {
            _categoryService = categoryService;
            _featureHandlerService = featureHandlerService;
        }

        [HttpGet("")]
        public ActionResult<IEnumerable<CategoryListDetailViewModel>> GetCategoryList()
        {
            return Ok(_categoryService.GetAllCategories());
        }

        [HttpGet("{id}/features")]
        public ActionResult<IEnumerable<CategoryFeaturesListDetailViewModel>> GetCategoryFeaturesList(int id)
        {
            if (!_categoryService.IsCategoryExists(id)) return NotFound();

            return Ok(_featureHandlerService.GetFeaturesList(id));
        }
    }
}