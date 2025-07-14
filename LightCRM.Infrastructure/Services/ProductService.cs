using LightCRM.Domain.Interface;
using LightCRM.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace LightCRM.Infrastructure.Services
{
    public class ProductService : IProductService
    {
        private readonly ApplicationDbContext _ctx;
        public ProductService(ApplicationDbContext ctx)
        {
            _ctx = ctx;
        }

        // Create
        public async Task<(Product? product, string error)> CreateAsync(string name, decimal price, int stock)
        {
            try
            {
                if (await _ctx.Products.FirstOrDefaultAsync(x => x.Name == name) != null)
                {
                    return (null, "Product with the same name already exists.");
                }

                Product model = new Product(Guid.NewGuid(), name, price, stock);

                await _ctx.Products.AddAsync(model);
                await _ctx.SaveChangesAsync();

                return (model, string.Empty);
            }
            catch (Exception ex)
            {
                return (null, ex.Message);
            }
        }

        // Read
        public async Task<(Product? product, string error)> ReadAsync(Guid id)
        {
            try
            {
                Product? entity = await _ctx.Products.FirstOrDefaultAsync(x => x.Id == id);
                if (entity == null)
                {
                    return (null, "Product does not exist.");
                }

                return (entity, string.Empty);
            }
            catch (Exception ex)
            {
                return (null, ex.Message);
            }
        }

        public async Task<(List<Product>? products, string error)> ReadAsync()
        {
            try
            {
                return (await _ctx.Products.ToListAsync(), string.Empty);
            }
            catch (Exception ex)
            {
                return (null, ex.Message);
            }
        }

        // Update
        public async Task<(Product? product, string error)> UpdateAsync(Guid id, string name, decimal price, int stock)
        {
            try
            {
                Product? entity = await _ctx.Products.FirstOrDefaultAsync(x => x.Id == id);
                if (entity == null)
                {
                    return (null, "Product does not exist.");
                }

                entity.Update(name, price, stock);
                await _ctx.SaveChangesAsync();

                return (entity, string.Empty);
            }
            catch (Exception ex)
            {
                return (null, ex.Message);
            }
        }

        // Delete
        public async Task<(Product? product, string error)> DeleteAsync(Guid id)
        {
            try
            {
                Product? entity = await _ctx.Products.FirstOrDefaultAsync(x => x.Id == id);
                if (entity == null)
                {
                    return (null, "Product does not exist.");
                }

                _ctx.Products.Remove(entity);
                await _ctx.SaveChangesAsync();

                return (entity, string.Empty);
            }
            catch (Exception ex)
            {
                return (null, ex.Message);
            }
        }
    }
}
