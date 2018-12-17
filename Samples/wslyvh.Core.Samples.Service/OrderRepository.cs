using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace wslyvh.Core.Samples.Service
{
    public class OrderRepository : IOrderRepository
    {
        private Collection<Order> _orders;

        public IEnumerable<Order> Orders
        {
            get { return _orders; }
        }

        public OrderRepository()
        {
            _orders = new Collection<Order>() { CreateRandomOrder(), CreateRandomOrder(), CreateRandomOrder() };
        }

        public void AddOrder(Order order)
        {
            _orders.Add(order);
        }

        public Order GetOrder(Guid id)
        {
            return Orders.FirstOrDefault(i => i.Id == id);
        }

        public void DeleteOrder(Guid id)
        {
            var first = this._orders.FirstOrDefault(i => i.Id == id);
            _orders.Remove(first);
        }

        private Order CreateRandomOrder()
        {
            var id = Guid.NewGuid();
            return new Order()
            {
                Id = id,
                Name = "Order " + id,
                Created = DateTime.Now,
                Status = OrderStatus.New
            };
        }
    }
}
