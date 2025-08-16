using LightCRM.API.Contracts;
using LightCRM.Domain.Models;

namespace LightCRM.API.Mappers
{
    public static class Mappers
    {
        // ===============================
        // ClientEntity -> ClientResponse
        public static ClientResponse ToResponse(this Client e)
        {
            return new ClientResponse(e.Id, e.Name, e.Email, e.Phone, e.Orders.Select(x => x.ToResponse()).ToList());
        }

        // ===============================
        // OrderEntity -> OrderResponse
        public static OrderResponse ToResponse(this Order e)
        {
            return new OrderResponse(e.Id, e.ClientId, e.CreatedAt, e.Items.Select(x => x.ToResponse()).ToList());
        }

        // ===============================
        // OrderItemEntity -> OrderItemResponse
        public static OrderItemResponse ToResponse(this OrderItem e) 
        {
            return new OrderItemResponse(e.Id, e.OrderId, e.ProductId, e.Quantity);
        }

        // ===============================
        // ProductEntity -> ProductResponse
        public static ProductResponse ToResponse(this Product e)
        {
            return new ProductResponse(e.Id, e.Name, e.Price, e.Stock);
        }


        // 
        //

        //
        // OrderItemRequest -> OrderItemEntity
        public static OrderItem FromRequest(this OrderItemRequest e)
        {
            return new OrderItem(Guid.Empty, Guid.Empty, e.ProductId, e.Quantity);
        }
    }
}
