using AppLibrary.DTOs.Category;
using AppLibrary.IService;
using AutoMapper;
using DomainLibrary.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ECommerceAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _categoryService;
        private readonly IMapper _mapper;
        public CategoryController(ICategoryService categoryService, IMapper mapper)
        {
            _categoryService = categoryService;
            _mapper = mapper;
        }

        [HttpGet("All")]
        public async Task<IActionResult> GetAllCategories()
        {
            var res = await _categoryService.GetAllCategoriesAsync();
            if (res == null || res.Count == 0)
            {
                return NotFound("There are no categories");
            }
            return Ok(res);
        }

        [HttpGet("CategoryBy{id}")]
        public async Task<IActionResult> GetCategoryById(int id)
        {
            var res = await _categoryService.GetCategoryByIdAsync(id);
            if (res == null)
            {
                return NotFound("Category by this id does not exist!");
            }
            return Ok(res);
        }


        [HttpGet("CategoryByProduct{id}")]
        public async Task<IActionResult> GetCategoryByProductId(int id)
        {
            var res = await _categoryService.GetCategoryWithProductsAsync(id);
            if (res == null)
            {
                return BadRequest("Invalid parameter");
            }
            return Ok(res);
        }

        [HttpPost("Add")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> AddNewCategory([FromBody] CategoryCreateDto categoryDto)
        {
            if (categoryDto == null || string.IsNullOrWhiteSpace(categoryDto.Name))
            {
                return BadRequest("Invalid category data.");
            }

            var category = _mapper.Map<Category>(categoryDto);
            var newCategory = await _categoryService.AddNewCategoryAsync(category);

            var categoryResponse = _mapper.Map<CategoryCreateDto>(newCategory);

            return CreatedAtAction(nameof(GetCategoryById), new { id = newCategory.Id }, categoryResponse);
        }

        [HttpDelete("DeleteBy{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteCategory(int id)
        {
            var category = await _categoryService.GetCategoryByIdAsync(id);

            if (category == null)
            {
                return BadRequest("Can't find category with given id");
            }
            await _categoryService.DeleteCategoryAsync(category);
            return Ok("Succesfully deleted category");
        }
    }
}