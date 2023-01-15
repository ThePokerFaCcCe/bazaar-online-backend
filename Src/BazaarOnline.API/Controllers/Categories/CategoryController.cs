using BazaarOnline.Application.Interfaces.Categories;
using BazaarOnline.Application.ViewModels.Categories;
using Microsoft.AspNetCore.Mvc;

namespace BazaarOnline.API.Controllers.Categories
{
    [Route("api/v1/categories")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpGet("")]
        public ActionResult<IEnumerable<CategoryListDetailViewModel>> GetCategoryList()
        {
            return Ok(_categoryService.GetAllCategories());
        }
    }
}