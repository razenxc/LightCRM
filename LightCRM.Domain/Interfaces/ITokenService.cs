using LightCRM.Domain.Models.Misc;

namespace LightCRM.Domain.Interfaces;

public interface ITokenService
{
    Task<(string? token, string error)> GenerateAccessTokenAsync(string email);
    Task<(string? token, string error)> GenerateRefreshTokenAsync(Guid userId);
    Task<(JwtTokens? tokensPair, string error)> RefreshTokensAsync(string refreshToken);
}