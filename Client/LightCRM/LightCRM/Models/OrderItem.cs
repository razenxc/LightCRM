using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LightCRM.Models
{
    public class OrderItem
    {
        public Guid Id { get; private set; }
        public Guid OrderId { get; private set; }
        public Order Order { get; private set; }
        public Guid ProductId { get; private set; }
        public Product Product { get; private set; }
        public int Quantity { get; private set; }

        public OrderItem(Guid id, Guid orderId, Guid productId, int quantity)
        {
            Id = id;
            OrderId = orderId;
            ProductId = productId;
            Quantity = quantity;
        }
    }
}
