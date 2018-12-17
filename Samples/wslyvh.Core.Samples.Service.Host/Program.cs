using System;
using System.ServiceModel;
using Microsoft.Practices.Unity;
using wslyvh.Core.Service.Host.Unity;

namespace wslyvh.Core.Samples.Service.Host
{
    public class Program
    {
        static void Main(string[] args)
        {
            var container = new UnityContainer();
            container.RegisterType<IOrderRepository, OrderRepository>();
            container.RegisterType<IOrderService, OrderService>();

            var baseAddress = new Uri("http://localhost:8080/Orders");
            using (var serviceHost = new UnityServiceHost(container, typeof(OrderService), baseAddress))
            {
                try
                {
                    // Might need to run: // netsh http add urlacl url=http://+:8080/ user=DOMAIN\user
                    serviceHost.Open();

                    Console.WriteLine("The service is ready.");
                    Console.WriteLine("Press <ENTER> to terminate service.");
                    Console.ReadLine();

                    serviceHost.Close();
                }
                catch (TimeoutException timeProblem)
                {
                    Console.WriteLine(timeProblem.Message);
                    Console.ReadLine();
                }
                catch (CommunicationException commProblem)
                {
                    Console.WriteLine(commProblem.Message);
                    Console.ReadLine();
                }
            }
        }
    }
}
