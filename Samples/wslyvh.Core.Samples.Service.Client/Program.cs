using System;
using System.ServiceModel;

namespace wslyvh.Core.Samples.Service.Client
{
    public class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Wait for Host to be ready..");
            Console.ReadKey();

            var binding = new BasicHttpBinding();
            var endpoint = new EndpointAddress("http://localhost:8080/Orders");

            using (var factory = new ChannelFactory<IOrderService>(binding, endpoint))
            {
                var proxy = factory.CreateChannel();
                var orders = proxy.GetOrders();
                ((IClientChannel)proxy).Close();

                foreach (var order in orders)
                {
                    Console.WriteLine(order.Name);
                }
            }

            Console.ReadKey();
        }
    }
}
