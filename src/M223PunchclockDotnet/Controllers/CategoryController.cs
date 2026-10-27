using System.Net.Mime;
using M223PunchclockDotnet.Model;
using M223PunchclockDotnet.Service;
using Microsoft.AspNetCore.Mvc;

namespace M223PunchclockDotnet.Controllers
{
    [ApiController]
    [Route("categories")]
    public class CategoryController : ControllerBase
    {
        private readonly CategoryService _categoryService;

        public CategoryController(CategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpGet]
        [ProducesResponseType<Category>(StatusCodes.Status200OK)]
        public async Task<IActionResult> Get()
        {
            var allCategories = await _categoryService.FindAll();
            return Ok(allCategories);
        }

        [HttpPost()]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType<Category>(StatusCodes.Status201Created)]
        public async Task<ActionResult<Category>> AddCategory(Category category)
        {
            var newElement = await _categoryService.AddCategory(category);

            return CreatedAtAction(nameof(Get), new { id = category.Id }, category);
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> DeleteCategory(int id)
        {
            var Category = await _categoryService.FindById(id);
            if (Category == null)
            {
                return NotFound();
            }

            await _categoryService.DeleteCategory(Category);
            return Ok();
        }

        [HttpPut("{id}")]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Category>> UpdateCategory(int id, Category updatedCategory)

        {
            var Category = await _categoryService.FindById(id);
            if (Category == null)
            {
                return NotFound();
            }

            Category.Title = updatedCategory.Title;
            Category.Entries = updatedCategory.Entries;

            await _categoryService.UpdateCategory(Category);

            return Ok(Category);
        }


    }
}
