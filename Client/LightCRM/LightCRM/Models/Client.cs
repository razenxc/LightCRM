using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Avalonia.Controls;

namespace LightCRM.Models
{
    public class Client
    {
        public Guid Id { get; private set; }
        public string Name { get; private set; }
        public string Email { get; private set; }
        public string Phone { get; private set; }
        public ICollection<Order> Orders { get; private set; }

        public Client(Guid id, string name, string email, string phone)
        {
            Id = id;
            Name = name;
            Email = email;
            Phone = phone;
            Orders = new List<Order>();
        }

        public void AddOrder(Order order)
        {
            Orders.Add(order);
        }

        public void RemoveOrder(Order order)
        {
            Orders.Remove(order);
        }

        public void Update(string name, string email, string phone)
        {
            Name = name;
            Email = email;
            Phone = phone;
        }
    }
}
