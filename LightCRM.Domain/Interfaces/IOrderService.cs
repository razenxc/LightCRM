using LightCRM.Domain.Models;

namespace LightCRM.Domain.Interfaces;

public interface IOrderService
{
    Task<(Order? order, string error)> CreateAsync(Guid clientId, List<OrderItem> orderItems);
    Task<(List<Order>? order, string error)> ReadAsync();
    Task<(Order? order, string error)> ReadAsync(Guid id);
}