using LightCRM.Domain.Models;

namespace LightCRM.Domain.Interfaces
{
    public interface IClientService
    {
        Task<(Client? client, string error)> CreateAsync(string name, string email, string phone);
        Task<(Client? client, string error)> DeleteAsync(Guid id);
        Task<(List<Client>? client, string error)> ReadAsync();
        Task<(Client? client, string error)> ReadAsync(Guid id);
        Task<(Client? client, string error)> UpdateAsync(Guid id, string name, string email, string phone);
    }
}