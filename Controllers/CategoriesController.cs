using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using TestDtoInApi.Models;
using TestDtoInApi.Models.DTO.CategoryDTO;
using TestDtoInApi.Repository.CategoryRepository.Interface;

namespace TestDtoInApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IMapper _mapper;

        public CategoriesController(ICategoryRepository categoryRepository, IMapper mapper)
        {
            _categoryRepository = categoryRepository;
            _mapper = mapper;
        }

        // GET: api/Categories
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IEnumerable<SelectCategoryDto>>> GetAllCategories()
        {
            // Fetch all the categories from the respotory
            var categories = await _categoryRepository.GetAllAsync();

            // Map Categories to SelectCategoryDto
            var categoryDto = _mapper.Map<IEnumerable<SelectCategoryDto>>(categories);

            return Ok(categoryDto);
        }

        // GET: api/Categories/5
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<SelectCategoryDto>> GetCategoryById(int id)
        {
            if(id == null)
            {
                return BadRequest("Invalid Id");
            }

            // Fetch the Category By using Id from the Repository
            var category = await _categoryRepository.GetByIdAsync(id);

            // Check that return result is empty, If Yes then through HTTP 404 Error
            if(category == null)
            {
                return NotFound($"No Data found with Id {id}");
            }

            // If category found than map it to Dto Objects
            var categoryDto = _mapper.Map<SelectCategoryDto>(category);

            return Ok(categoryDto);
        }

        // POST: api/Categories
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<SelectCategoryDto>> CreateCategory([FromBody] CreateCategoryDto createCategoryDto)
        {
            if (createCategoryDto == null)
            {
                return BadRequest();
            }

            // Map the Dto to the Category entity
            var category = _mapper.Map<Category>(createCategoryDto);

            // Create the category using the Repository
            var createCategory = await _categoryRepository.CreateAsync(category);

            // Map the Created Category back to a Dto
            var categoryDto = _mapper.Map<SelectCategoryDto>(createCategory);

            return CreatedAtAction(nameof(GetCategoryById), new { id = createCategory.CategoryId }, categoryDto);
        }

        // PUT: api/Categories/3
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> UpdateCategory([FromBody] UpdateCategoryDto updateCategoryDto)
        {
            if (updateCategoryDto == null)
            {
                return BadRequest("Update data is required.");
            }

            // Map the DTO to the Category entity
            var category = _mapper.Map<Category>(updateCategoryDto);

            // Check if the category exists
            var existingCategory = await _categoryRepository.GetByIdAsync(updateCategoryDto.CategoryId);
            if (existingCategory == null)
            {
                return NotFound($"Category with ID {updateCategoryDto.CategoryId} not found.");
            }

            // Manually Update fields
            existingCategory.CategoryName = category.CategoryName;

            // Save Changes
            var result = await _categoryRepository.UpdateAsync(existingCategory);

            if (result == null)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error updating the category");
            }

            return NoContent();
        }

        // DELETE: api/Categories/5
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeleteCategory(int id)
        {
            // Retrieve the existing category
            var existingCategory = await _categoryRepository.GetByIdAsync(id);
            if(existingCategory == null)
            {
                return NotFound($"Category with ID {id} was not found.");
            }

            // Perform Delete Operation
            var result = await _categoryRepository.DeleteAsync(existingCategory);

            if(result == null)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error deleting the category.");
            }

            // Return no content status to indicate successful deletion
            return NoContent();
        }
    }
}
