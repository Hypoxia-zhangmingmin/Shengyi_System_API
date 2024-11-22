using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Shengyi_WebAPI.Models.In;
using Shengyi_WebAPI.Models.Out;
using Shengyi_WebAPI.Services.Category;

namespace Shengyi_WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpPost("new")]
        public async Task<object> AddCategory([FromBody] CreateCategoryInfo info)
        {
            var result = await _categoryService.AddCategory(info.CategoryName, info.Description);
            return ((bool)result) ? Ok(new OutputModel<object>().Success()) : Ok(new OutputModel<object>().Failed());
        }

        [HttpPost("search")]
        public async Task<object> GetCategory([FromBody] SearchCategoryInfo info)
        {
            var result = await _categoryService.SearchCategory(info.Name, info.CurrentPage);
            return Ok(new OutputModel<object>().Success(result));
        }

        [HttpPost("update")]
        public async Task<object> UpdateCategory([FromBody] EditCategoryInfo info)
        {
            var result = await _categoryService.EditCategory(info.Id, info.CategoryName, info.Description);
            return ((bool)result) ? Ok(new OutputModel<object>().Success()) : Ok(new OutputModel<object>().Failed());
        }

        [HttpPost("count")]
        public async Task<object> GetCategoryCount([FromBody] SearchCategoryInfo info)
        {
            var result = await _categoryService.GetCategoryCount(info.Name);
            return Ok(new OutputModel<object>().Success(result));
        }
        [HttpPost("delete")]
        public async Task<object> DeleteCategory([FromBody] DeleteCategoryInfo info)
        {
            var result = await _categoryService.DeleteCategory(info.Id);
            return ((bool)result) ? Ok(new OutputModel<object>().Success()) : Ok(new OutputModel<object>().Failed());
        }

        [HttpGet("all")]
        public async Task<object> GetAllCategory()
        {
            var result = await _categoryService.GetCategory();
            return Ok(new OutputModel<object>().Success(result));
        }
    }
}
