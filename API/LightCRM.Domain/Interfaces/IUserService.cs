using LightCRM.Domain.Models;
using LightCRM.Domain.Models.Misc;

namespace LightCRM.Domain.Interfaces;

public interface IUserService
{
    Task<(User? user, string error)> Register(string email, string password);
    Task<(JwtTokens? jwtTokens, string error)> Login(string email, string password);
}