using LightCRM.Domain.Interface;
using LightCRM.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace LightCRM.Infrastructure.Services
{
    public class ClientService : IClientService
    {
        private readonly ApplicationDbContext _ctx;
        public ClientService(ApplicationDbContext ctx)
        {
            _ctx = ctx;
        }

        // Create
        public async Task<(Client? client, string error)> CreateAsync(string name, string email, string phone)
        {
            try
            {
                if (await _ctx.Clients.FirstOrDefaultAsync(x => x.Email == email) != null)
                {
                    return (null, "Client already exists.");
                }

                Client client = new Client(Guid.NewGuid(), name, email, phone);

                await _ctx.Clients.AddAsync(client);
                await _ctx.SaveChangesAsync();

                return (client, string.Empty);
            }
            catch (Exception ex)
            {
                return (null, ex.Message);
            }
        }

        // Read
        public async Task<(Client? client, string error)> ReadAsync(Guid id)
        {
            try
            {
                Client? entity = await _ctx.Clients.FirstOrDefaultAsync();
                if (entity == null)
                {
                    return (null, "User does not exist.");
                }

                return (entity, string.Empty);
            }
            catch (Exception ex)
            {
                return (null, ex.Message);
            }
        }

        public async Task<(List<Client>? client, string error)> ReadAsync()
        {
            try
            {
                return (await _ctx.Clients.ToListAsync(), string.Empty);
            }
            catch (Exception ex)
            {
                return (null, ex.Message);
            }
        }

        // Update
        public async Task<(Client? client, string error)> UpdateAsync(Client client)
        {
            try
            {
                Client? entity = await _ctx.Clients.FirstOrDefaultAsync(x => x.Email == client.Email);
                if (entity == null)
                {
                    return (null, "Client does not exist.");
                }

                entity.Update(client.Name, client.Email, client.Phone);
                await _ctx.SaveChangesAsync();

                return (entity, string.Empty);
            }
            catch (Exception ex)
            {
                return (null, ex.Message);
            }
        }

        // Delete
        public async Task<(Client? client, string error)> DeleteAsync(Guid id)
        {
            try
            {
                Client? entity = await _ctx.Clients.FirstOrDefaultAsync(x => x.Id == id);
                if (entity == null)
                {
                    return (null, "Client does not exist.");
                }

                _ctx.Clients.Remove(entity);
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
