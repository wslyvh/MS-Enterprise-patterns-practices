using Microsoft.Practices.ServiceLocation;
using System;
using System.Linq;
using wslyvh.Core.Boot.Unity;
using wslyvh.Core.Configuration.Source;
using wslyvh.Core.Interfaces.ServiceClient.Enumerations;
using wslyvh.Core.Interfaces.ServiceClient.Rest;

namespace wslyvh.Core.Samples.ServiceClient
{
    using System.Collections.Generic;

    using wslyvh.Core.Samples.ServiceClient.Entities;

    public class Program
    {
        public static void Main(string[] args)
        {
            var configSource = new SystemConfigurationSource();
            var bootstrapperConfig = new UnityBootstrapperConfiguration(configSource);
            var bootstrapper = new UnityBootstrapper(bootstrapperConfig);
            bootstrapper.Startup();

            EventfulRestClient();
            Console.ReadLine();

            Console.WriteLine("Press a key to exit..");
            Console.ReadLine();
        }
        

        private static void EventfulRestClient()
        {
            var client = ServiceLocator.Current.GetInstance<ISyncRestClient>("Eventful");
            var result = client.Execute<EventfulData>(Method.GET, "/events/search?q=music&l=London&t=Last+Week&keywords=illusion");

            if (result == null) Console.WriteLine("No result.");
            else
            {
                Console.WriteLine("Result: ");

                Console.WriteLine("\t ErrorMessage: {0}", result.ErrorMessage);
                Console.WriteLine("\t Exception: {0}", result.Exception);
                Console.WriteLine("\t ResponseStatus: {0}", result.ResponseStatus);
                Console.WriteLine("\t StatusCode: {0}", result.StatusCode);
                Console.WriteLine("\t StatusDescription: {0}", result.StatusDescription);
                //Console.WriteLine("\t RawData: {0}", result.RawData);
                Console.WriteLine("\t ResponseUri: {0}", result.ResponseUri);
                Console.WriteLine("==================================================");

                Console.WriteLine("\n Result Data: ");
                foreach (var eventfulEvent in result.Data.Events)
                {
                    Console.WriteLine("\t Event: {0} ({1})", eventfulEvent.Title, eventfulEvent.Id);
                }
            }
        }
    }
}
