using AppLibrary.DTOs.Product;
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
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;
        private readonly IMapper _mapper;
        public ProductController(IProductService productService, IMapper mapper)
        {
            _productService = productService;
            _mapper = mapper;
        }

        [HttpGet("All")]
        public async Task<IActionResult> GetAll()
        {
            var res = await _productService.GetAllProductAsync();
            if (res == null || res.Count == 0)
            {
                return NotFound("There are no products!");
            }
            return Ok(res);
        }

        [HttpGet("GetByCategory{id}")]
        public async Task<IActionResult> GetByCategoryId(int id)
        {
            var res = await _productService.GetProductsByCategoryAsync(id);
            if (res == null || res.Count == 0)
            {
                return NotFound("No product found!");
            }
            return Ok(res);
        }

        [HttpGet("GetBy{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var res = await _productService.GetByIdAsync(id);
            if (res == null)
            {
                return NotFound("Product not found!");
            }
            return Ok(res);
        }


        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteById(int id)
        {
            var res = await _productService.GetByIdAsync(id);
            if (res == null)
            {
                return NotFound("Product not found!");
            }

            await _productService.DeleteProductAsync(res);
            return Ok("Product deleted succesfully");
        }

        [HttpPatch("id")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateById(int id, [FromBody] ProductUpdateDto productDto)
        {
            var product = await _productService.GetByIdAsync(id);
            if (product == null)
            {
                return NotFound("Product not found!");
            }

            _mapper.Map(productDto, product);

            await _productService.UpdateProductAsync(product);

            var updatedProductDto = _mapper.Map<ProductUpdateDto>(product);
            return Ok(updatedProductDto);
        }

        [HttpPost("id")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create([FromBody] ProductCreateDto productDto)
        {
            if (productDto == null || string.IsNullOrWhiteSpace(productDto.Name))
            {
                return BadRequest("Invalid product data");
            }

            var product = _mapper.Map<Product>(productDto);
            var newProduct = await _productService.AddNewProductAsync(product);

            var productResponse = _mapper.Map<ProductCreateDto>(product);
            return CreatedAtAction(nameof(GetById), new { id = newProduct.Id }, newProduct);
        }
    }
}