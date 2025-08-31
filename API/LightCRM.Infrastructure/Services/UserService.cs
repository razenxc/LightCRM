using LightCRM.Domain.Constants;
using LightCRM.Domain.Interfaces;
using LightCRM.Domain.Models;
using LightCRM.Domain.Models.Misc;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace LightCRM.Infrastructure.Services
{
    public class UserService : IUserService
    {
        private readonly ApplicationDbContext _ctx;
        private readonly ITokenService _tokenService;
        public UserService(ApplicationDbContext ctx, ITokenService tokenService)
        {
            _ctx = ctx;
            _tokenService = tokenService;
        }

        public async Task<(User? user, string error)> Register(string username, string password)
        {
            if (await _ctx.Users.FirstOrDefaultAsync(x => x.Username == username) != null)
            {
                return (null, "User is already exist!");
            }

            User user = new User(Guid.NewGuid(), username, UserRoles.User);

            PasswordHasher<User> hasher = new PasswordHasher<User>();

            user.SetPasswordHash(hasher.HashPassword(user, password));

            await _ctx.Users.AddAsync(user);
            await _ctx.SaveChangesAsync();

            return (user, string.Empty);
        }

        public async Task<(JwtTokens? jwtTokens, string error)> Login(string username, string password)
        {
            User? entity = await _ctx.Users.FirstOrDefaultAsync(x => x.Username == username);
            if (entity == null)
            {
                return (null, "User does not exist!");
            }

            PasswordHasher<User> hasher = new PasswordHasher<User>();

            if(hasher.VerifyHashedPassword(entity, entity.PasswordHash, password) != PasswordVerificationResult.Success)
            {
                return (null, "Wrong password or email");
            }

            (string? accessToken, string error) = await _tokenService.GenerateAccessTokenAsync(entity.Username);
            if (!string.IsNullOrEmpty(error)) 
            {
                return (null, error);
            }
            (string? refreshToken, error) = await _tokenService.GenerateRefreshTokenAsync(entity.Id);
            if (!string.IsNullOrEmpty(error))
            {
                return (null, error);
            }

            return (new JwtTokens { AccessToken = accessToken, RefreshToken = refreshToken }, string.Empty);
        }
    }
}
