using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using LightCRM.Domain.Interfaces;
using LightCRM.Domain.Models;
using LightCRM.Domain.Models.Misc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace LightCRM.Infrastructure.Services
{
    public class TokenService : ITokenService
    {
        private readonly ApplicationDbContext _ctx;
        private readonly IConfiguration _cfg;
        public TokenService(ApplicationDbContext ctx, IConfiguration cfg)
        {
            _ctx = ctx;
            _cfg = cfg;
        }

        public async Task<(string? token, string error)> GenerateRefreshTokenAsync(Guid userId)
        {
            JwtRefreshToken refreshToken = new(
                Guid.NewGuid(),
                Convert.ToBase64String(RandomNumberGenerator.GetBytes(64)),
                userId,
                DateTime.UtcNow.AddMinutes(Convert.ToInt32(_cfg["Jwt:RefreshTokenExpiresIn"]))
                );
            refreshToken.SetRevoked(false);

            await _ctx.JwtRefreshTokens.AddAsync(refreshToken);
            await _ctx.SaveChangesAsync();

            return (refreshToken.RefreshToken, string.Empty);
        }

        public async Task<(string? token, string error)> GenerateAccessTokenAsync(string username)
        {
            User? entity = await _ctx.Users.FirstOrDefaultAsync(x => x.Username == username);
            if (entity == null)
            {
                return (null, "User not found!");
            }

            List<Claim> claims = new()
            {
                new Claim(ClaimTypes.NameIdentifier, entity.Id.ToString()),
                new Claim(ClaimTypes.Role, entity.Role),
                new Claim(ClaimTypes.GivenName, entity.Username)
            };

            SymmetricSecurityKey key = new(Encoding.UTF8.GetBytes(_cfg["Jwt:SigningKey"]));

            SigningCredentials credentials = new(key, SecurityAlgorithms.HmacSha256);

            JwtSecurityToken accessToken = new(
                _cfg["Jwt:Issuer"],
                _cfg["Jwt:Audience"],
                claims,
                expires: DateTime.UtcNow.AddMinutes(Convert.ToInt32(_cfg["Jwt:AccessTokenExpiresIn"])),
                signingCredentials: credentials
                );

            return (new JwtSecurityTokenHandler().WriteToken(accessToken), string.Empty);
        }

        // refresh tokens async
        public async Task<(JwtTokens? tokensPair, string error)> RefreshTokensAsync(string refreshToken)
        {
            JwtRefreshToken? oldRefresh = await _ctx.JwtRefreshTokens.FirstOrDefaultAsync(x => x.RefreshToken == refreshToken);
            User? user = await _ctx.Users.FirstOrDefaultAsync(x => x.Id == oldRefresh.UserId);

            if (oldRefresh == null) return (null, "Refresh token does not exist.");
            if (oldRefresh.IsRevoked) return (null, "Refresh token is revoked");
            if (oldRefresh.ExpireAt < DateTime.UtcNow) return (null, "Refresh token is expired");
            if (user == null) return (null, "User does not exists.");

            (string? newAccessToken, string error) = await GenerateAccessTokenAsync(user.Username);
            if (error == null) return (null, "Something went wrong with access token creation");
            (string? newRefreshToken, error) = await GenerateRefreshTokenAsync(user.Id);
            if (error == null) return (null, "Something went wrong with refresh token creation");

            oldRefresh.SetRevoked(true);
            await _ctx.SaveChangesAsync();

            return (new JwtTokens { AccessToken = newAccessToken, RefreshToken = newRefreshToken }, string.Empty);
        }
    }
}
