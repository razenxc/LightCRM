using LightCRM.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace LightCRM.Infrastructure
{
    public class ApplicationDbContext : DbContext
    {
        private readonly IConfiguration _config;

        public ApplicationDbContext(DbContextOptions o, IConfiguration config) : base(o)
        {
            _config = config;
        }

        public DbSet<Client> Clients { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<JwtRefreshToken> JwtRefreshTokens { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql(_config.GetConnectionString("Default"));
        }
    }
}
