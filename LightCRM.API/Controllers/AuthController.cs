using LightCRM.API.Contracts;
using LightCRM.Domain.Interfaces;
using LightCRM.Domain.Models;
using LightCRM.Domain.Models.Misc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LightCRM.API.Controllers
{
    [AllowAnonymous]
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly ITokenService _tokenService;
        public AuthController(IUserService userService, ITokenService tokenService)
        {
            _userService = userService;
            _tokenService = tokenService;
        }

        [HttpPost("register")]
        public async Task<ActionResult> Register([FromBody] UserRequest req)
        {
            (User? user, string error) = await _userService.Register(req.Email, req.Password);
            if (!string.IsNullOrEmpty(error))
            {
                return BadRequest(error);
            }
            return Ok();
        }

        [HttpPost("login")]
        public async Task<ActionResult<JwtTokens>> Login([FromBody] UserRequest req)
        {
            (JwtTokens? tokens, string error) = await _userService.Login(req.Email, req.Password);
            if (!string.IsNullOrEmpty(error))
            {
                return BadRequest(error);
            }
            return Ok(tokens);
        }

        [HttpPost("refreshTokens")]
        public async Task<ActionResult<JwtTokens>> RefreshTokens([FromBody] RefreshTokensRequest req)
        {
            (JwtTokens? tokens, string error) = await _tokenService.RefreshTokensAsync(req.oldRefreshToken);
            if (!string.IsNullOrEmpty(error))
            {
                return BadRequest(error);
            }
            return Ok(tokens);
        }
    }
}
