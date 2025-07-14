using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LightCRM.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace LightCRM.Infrastructure.Services
{
    public class OrderService
    {
        private readonly ApplicationDbContext _ctx;
        public OrderService(ApplicationDbContext ctx) 
        {
            _ctx = ctx;
        }

        public async Task<(Order? order, string error)> CreateAsync(Guid clientId, List<OrderItem> orderItems)
        {
            try
            {
                Order model = new Order(Guid.NewGuid(), clientId, DateTime.UtcNow);

                foreach (OrderItem item in orderItems)
                {
                    model.AddItem(item);
                }

                await _ctx.Orders.AddAsync(model);
                await _ctx.SaveChangesAsync();

                return (model, string.Empty);
            }
            catch (Exception ex)
            {
                return (null, ex.Message);
            }
        }

        public async Task<(Order? order, string error)> ReadAsync(Guid id)
        {
            try
            {
                Order? entity = await _ctx.Orders.Include(x => x.Items).FirstOrDefaultAsync(x => x.Id == id);
                if (entity == null)
                {
                    return (null, "Order does not exist!");
                }

                return (entity, string.Empty);
            }
            catch (Exception ex)
            {
                return (null, ex.Message);
            }
        }

        public async Task<(List<Order>? order, string error)> ReadAsync()
        {
            try
            {
                return (await _ctx.Orders.Include(x => x.Items).ToListAsync(), string.Empty);
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
