using LightCRM.API.Contracts;
using LightCRM.API.Mappers;
using LightCRM.Domain.Constants;
using LightCRM.Domain.Interfaces;
using LightCRM.Domain.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LightCRM.API.Controllers
{
    [Authorize(Roles = UserRoles.Admin)]
    [Authorize(Roles = UserRoles.SuperAdmin)]
    [Route("api/orders")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly IOrderService _service;
        public OrdersController(IOrderService service)
        {
            _service = service;
        }

        [HttpPost]
        public async Task<ActionResult<OrderResponse>> Create([FromBody] OrderRequest req)
        {
            (Order? order, string error) = await _service.CreateAsync(req.ClientId, req.Items.Select(x => x.FromRequest()).ToList());
            if (!string.IsNullOrEmpty(error))
            {
                return BadRequest(error);
            }
            return Ok(order.ToResponse());
        }

        [HttpGet]
        public async Task<ActionResult<OrderResponse>> Read() 
        {
            (List<Order>? order, string error) = await _service.ReadAsync();
            if (!string.IsNullOrEmpty(error))
            {
                return BadRequest(error);
            }
            return Ok(order.Select(x => x.ToResponse()));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<OrderResponse>> Read([FromRoute] Guid id)
        {
            (Order? order, string error) = await _service.ReadAsync(id);
            if (!string.IsNullOrEmpty (error))
            {
                return BadRequest(error);
            }
            return Ok(order.ToResponse());
        }
    }
}
