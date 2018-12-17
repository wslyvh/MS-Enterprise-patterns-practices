using System;
using System.Collections.Generic;

namespace wslyvh.Core.Samples.Service
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _repository;

        public OrderService(IOrderRepository repository)
        {
            _repository = repository;
        }

        public bool IsAlive
        {
            get { return true; }
        }

        public void AddOrder(Order order)
        {
            _repository.AddOrder(order);
        }

        public Order GetOrder(Guid id)
        {
            return _repository.GetOrder(id);
        }

        public IEnumerable<Order> GetOrders()
        {
            return _repository.Orders;
        }
    }
}
