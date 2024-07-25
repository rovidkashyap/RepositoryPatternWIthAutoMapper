using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using TestDtoInApi.Models;
using TestDtoInApi.Models.DTO.ProductDTO;
using TestDtoInApi.Models.Pagination.Products;
using TestDtoInApi.Repository.ProductRepository.Interface;

namespace TestDtoInApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;

        public ProductsController(IProductRepository productRepository, IMapper mapper)
        {
            _productRepository = productRepository;
            _mapper = mapper;
        }

        // GET: api/Products
        //[HttpGet]
        //public async Task<ActionResult<IEnumerable<SelectProductDto>>> GetAllProducts()
        //{
        //    // Fetch the Products from Repository
        //    
        //    var products = await _productRepository.GetAllAsync(p => p.Category);
        //
        //    //  Map the Products to Dto
        //    var productsDto = _mapper.Map<IEnumerable<SelectProductDto>>(products);
        //
        //    return Ok(productsDto);
        //}

        // Pagination Result
        [HttpGet]
        public async Task<ActionResult<PaginatedList<SelectProductDto>>> GetAllProductsByPagination(int pageNumber = 1, int pageSize = 10)
        {
            var products = await _productRepository.GetProductsAsync(pageNumber, pageSize);
            var productsDto = _mapper.Map<IEnumerable<SelectProductDto>>(products);

            var paginatedList = new PaginatedList<SelectProductDto>(productsDto.ToList(), products.Count, pageNumber, pageSize);

            return Ok(paginatedList);
        }

        // GET: api/Products/1
        [HttpGet("{id}")]
        public async Task<ActionResult<SelectProductDto>> GetProductById(int id)
        {
            if (id == null)
            {
                return BadRequest("Invalid Id");
            }

            // Fetch the Product by using the Repository
            var product = await _productRepository.GetByIdAsync(id);

            // Check the return result is empty, If Yes, then Show HTTP 404 Error
            if(product == null)
            {
                return NotFound($"No Data found with Id {id}");
            }

            // If product found than map it to Dto objects
            var productDto = _mapper.Map<SelectProductDto>(product);

            return Ok(productDto);
        }

        // POST: api/Products
        [HttpPost]
        public async Task<ActionResult<CreateProductDto>> CreateProduct([FromBody] CreateProductDto createProductDto)
        {
            if (createProductDto == null)
            {
                return BadRequest();
            }

            // Map the Dto to the Product entity
            var product = _mapper.Map<Product>(createProductDto);

            // Create the Product using the Repository
            var createProduct = await _productRepository.CreateAsync(product);

            // Map the Create Product back to the Dto
            var productDto = _mapper.Map<CreateProductDto>(createProduct);

            return CreatedAtAction(nameof(GetProductById), new { id = createProduct.ProductId }, productDto);
        }

        // PUT: api/Products/2
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProduct([FromBody] UpdateProductDto updateProductDto)
        {
            if(updateProductDto == null)
            {
                return BadRequest("Update data is required.");
            }

            // Map the Dto to the Product Entity
            var product = _mapper.Map<Product>(updateProductDto);

            // Check if the Product exists
            var existingProduct = await _productRepository.GetByIdAsync(updateProductDto.ProductId);
            if(existingProduct == null)
            {
                return NotFound($"Product with ID {updateProductDto.ProductId} was not found.");
            }

            // Manually update fields
            existingProduct.ProductName = product.ProductName;
            existingProduct.ProductPrice = product.ProductPrice;
            existingProduct.InStock = product.InStock;
            existingProduct.CategoryId = product.CategoryId;

            // Save Changes
            var result = await _productRepository.UpdateAsync(existingProduct);

            if (result == null)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error updating the product");
            }

            return NoContent();
        }

        // DELETE: api/Products/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            // Retrieve the existing product
            var existingProduct = await _productRepository.GetByIdAsync(id);
            if(existingProduct == null)
            {
                return NotFound($"Product with ID {id} was not Found.");
            }

            // Peform Delete Operation
            var result = await _productRepository.DeleteAsync(existingProduct);

            if(result == null)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error deleting the product.");
            }

            // Return no content status to indicate successful deletion
            return NoContent();
        }

        
    }
}
