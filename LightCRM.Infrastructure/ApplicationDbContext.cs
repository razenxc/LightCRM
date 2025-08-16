using LightCRM.Domain.Constants;
using LightCRM.Domain.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace LightCRM.Infrastructure
{
    public class ApplicationDbContext : DbContext
    {
        private readonly IConfiguration _config;
        private readonly IPasswordHasher<User> _passwordHasher;

        public ApplicationDbContext(DbContextOptions o, IConfiguration config) : base(o)
        {
            _config = config;
            _passwordHasher = new PasswordHasher<User>();
        }

        public DbSet<Client> Clients { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<JwtRefreshToken> JwtRefreshTokens { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) => optionsBuilder
            .UseNpgsql(_config.GetConnectionString("Default"))
            .UseSeeding((ctx, _) =>
            {
                string adminEmail = _config["Api:AdminEmail"];
                string adminPassword = _config["Api:AdminPassword"];

                if (ctx.Set<User>().FirstOrDefault(x => x.Role == UserRoles.SuperAdmin) == null) // There should only be one user with the SUPERADMIN role.
                {
                    User defaultAdminAccount = new User(Guid.NewGuid(), adminEmail, UserRoles.SuperAdmin);
                    defaultAdminAccount.SetPasswordHash(_passwordHasher.HashPassword(defaultAdminAccount, adminPassword));
                    ctx.Set<User>().Add(defaultAdminAccount);
                    ctx.SaveChanges();
                }
            });
    }
}
