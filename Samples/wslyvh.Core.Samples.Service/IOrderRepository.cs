using System;
using System.Collections.Generic;

namespace wslyvh.Core.Samples.Service
{
    public interface IOrderRepository
    {
        IEnumerable<Order> Orders { get; }

        void AddOrder(Order order);

        Order GetOrder(Guid id);

        void DeleteOrder(Guid id);
    }
}
