using LightCRM.API.Contracts;
using LightCRM.API.Mappers;
using LightCRM.Domain.Constants;
using LightCRM.Domain.Interfaces;
using LightCRM.Domain.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LightCRM.API.Controllers
{
    [Authorize(Roles = $"{UserRoles.Admin},{UserRoles.SuperAdmin}")]
    [Route("api/clients")]
    [ApiController]
    public class ClientsController : ControllerBase
    {
        private readonly IClientService _service;
        public ClientsController(IClientService service)
        {
            _service = service;
        }

        [HttpGet("{id:guid}")]
        public async Task<ActionResult<ClientResponse>> Read([FromRoute] Guid id)
        {
            (Client? client, string error) = await _service.ReadAsync(id);
            if (!string.IsNullOrEmpty(error))
            {
                return BadRequest(error);
            }
            return Ok(client.ToResponse());
        }

        [HttpGet]
        public async Task<ActionResult<List<ClientResponse>>> Read()
        {
            (List<Client>? client, string error) = await _service.ReadAsync();
            if (!string.IsNullOrEmpty(error))
            {
                return BadRequest(error);
            }
            return Ok(client.Select(x => x.ToResponse()));
        }

        [HttpPost]
        public async Task<ActionResult<ClientResponse>> Create([FromBody] ClientRequest req)
        {
            (Client? client, string error) = await _service.CreateAsync(req.Name, req.Email, req.Phone);
            if (!string.IsNullOrEmpty(error))
            {
                return BadRequest(error);
            }
            return Ok(client);
        }

        [HttpPut("{id:guid}")]
        public async Task<ActionResult<ClientResponse>> Update([FromRoute] Guid id, [FromBody] ClientRequest req)
        {
            (Client? client, string error) = await _service.UpdateAsync(id, req.Name, req.Email, req.Phone);
            if (!string.IsNullOrEmpty(error))
            {
                return BadRequest(error);
            }
            return Ok(client.ToResponse());
        }

        [HttpDelete("{id:guid}")]
        public async Task<ActionResult> Delete([FromRoute] Guid id)
        {
            (Client? client, string error) = await _service.DeleteAsync(id);
            if (!string.IsNullOrEmpty(error))
            {
                return BadRequest(error);
            }
            return NoContent();
        }
    }
}
