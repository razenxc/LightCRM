using LightCRM.Domain.Models;

namespace LightCRM.Domain.Interface
{
    public interface IProductService
    {
        Task<(Product? product, string error)> CreateAsync(string name, decimal price, int stock);
        Task<(Product? product, string error)> DeleteAsync(Guid id);
        Task<(List<Product>? products, string error)> ReadAsync();
        Task<(Product? product, string error)> ReadAsync(Guid id);
        Task<(Product? product, string error)> UpdateAsync(Guid id, string name, decimal price, int stock);
    }
}