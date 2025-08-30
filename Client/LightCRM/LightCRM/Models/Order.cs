using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LightCRM.Models
{
    public class Order
    {
        public Guid Id { get; private set; }
        public Guid ClientId { get; private set; }
        public Client Client { get; private set; }
        public DateTime CreatedAt { get; private set; }
        public ICollection<OrderItem> Items { get; private set; }

        public Order(Guid id, Guid clientId, DateTime createdAt)
        {
            Id = id;
            ClientId = clientId;
            CreatedAt = createdAt;
            Items = new List<OrderItem>();
        }

        public void AddItem(OrderItem item)
        {
            Items.Add(item);
        }

        public void RemoveItem(OrderItem item)
        {
            Items.Remove(item);
        }
    }
}
