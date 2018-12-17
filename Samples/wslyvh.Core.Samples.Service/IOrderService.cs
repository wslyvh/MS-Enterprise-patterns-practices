using System;
using System.Collections.Generic;
using System.ServiceModel;
using wslyvh.Core.Service;

namespace wslyvh.Core.Samples.Service
{
    [ServiceContract]
    public interface IOrderService : IService
    {
        [OperationContract]
        void AddOrder(Order order);

        [OperationContract]
        Order GetOrder(Guid id);

        [OperationContract]
        IEnumerable<Order> GetOrders();
    }
}
