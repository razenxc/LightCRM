using LightCRM.API.Contracts;
using LightCRM.API.Mappers;
using LightCRM.Domain.Interfaces;
using LightCRM.Domain.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LightCRM.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "ADMIN")]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _service;
        public ProductsController(IProductService service)
        {
            _service = service;
        }

        [HttpPost]
        public async Task<ActionResult<ProductResponse>> Create([FromBody] ProductRequest req)
        {
            (Product? product, string error) = await _service.CreateAsync(req.Name, req.Price, req.Stock);
            if (!string.IsNullOrEmpty(error))
            {
                return BadRequest(error);
            }
            return Ok(product.ToResponse());
        }

        [HttpGet("{id:guid}")]
        public async Task<ActionResult<ProductResponse>> Read([FromRoute] Guid id)
        {
            (Product? product, string error) = await _service.ReadAsync(id);
            if (!string.IsNullOrEmpty(error))
            {
                return BadRequest(error);
            }
            return Ok(product.ToResponse());
        }

        [HttpGet]
        public async Task<ActionResult<List<ProductResponse>>> Read()
        {
            (List<Product>? product, string error) = await _service.ReadAsync();
            if (!string.IsNullOrEmpty(error))
            {
                return BadRequest(error);
            }
            return Ok(product.Select(x => x.ToResponse()));
        }

        [HttpPut("{id:guid}")]
        public async Task<ActionResult<ProductResponse>> Update([FromRoute] Guid id, [FromBody] ProductRequest req)
        {
            (Product? product, string error) = await _service.UpdateAsync(id, req.Name, req.Price, req.Stock);
            if (!string.IsNullOrEmpty(error))
            {
                return BadRequest(error);
            }
            return Ok(product.ToResponse());
        }

        [HttpDelete("{id:guid}")]
        public async Task<ActionResult> Delete([FromRoute] Guid id)
        {
            (Product? product, string error) = await _service.DeleteAsync(id);
            if (!string.IsNullOrEmpty(error))
            {
                return BadRequest(error);
            }
            return NoContent();
        }
    }
}
