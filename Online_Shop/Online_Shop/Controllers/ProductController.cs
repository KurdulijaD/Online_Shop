using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Online_Shop.Dto;
using Online_Shop.Interfaces.ServiceInterfaces;
using System.Data;
using System.IdentityModel.Tokens.Jwt;

namespace Online_Shop.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _service;

        public ProductController(IProductService service)
        {
            _service = service;
        }

        //GET api/product
        [HttpGet]
        //[Authorize(Roles = "SALESMAN")]
        public async Task<IActionResult> Get()
        {
            List<ProductDto> users = await _service.GetAllProducts();
            if (users == null)
                return BadRequest();
            return Ok(users);
        }

        //GET api/product/id
        [HttpGet("{id}")]
        //[Authorize(Roles = "SALESMAN")]
        public async Task<IActionResult> Get(int id)
        {
            ProductDto productDto = await _service.GetProductById(id);
            if (productDto == null)
                return BadRequest();
            return Ok(productDto);
        }

        //POST api/product
        [HttpPost]
        //[Authorize(Roles = "SALESMAN")]
        public async Task<IActionResult> Post([FromBody] CreateProductDto productDto)
        {
            int id = int.Parse(User.Claims.First(c => c.Type == "UserId").Value);
            ProductDto product = await _service.CreateProduct(id, productDto);
            if (product == null)
                return BadRequest();
            return Ok(product);
        }

        //PUT api/product
        [HttpPut]
        //[Authorize(Roles = "SALESMAN")]
        public async Task<IActionResult> Put([FromBody] UpdateProductDto productDto)
        {
            int id = int.Parse(User.Claims.First(c => c.Type == "UserId").Value);
            ProductDto product = await _service.UpdateProduct(id, productDto);
            if (product == null)
                return BadRequest();
            return Ok(product);
        }

        //DELETE api/product/id
        [HttpDelete]
        //[Authorize(Roles = "SALESMAN")]
        public async Task<IActionResult> Delete(int id)
        {
            bool deleted = await _service.DeleteProduct(id);
            if (deleted == false)
                return BadRequest();
            return Ok();
        }
    }
}
