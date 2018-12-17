using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace wslyvh.Core.Interfaces.IoC
{    
    public interface IDependencyResolverFactory
     {
         /// <summary>
         /// Creates the resolver.
         /// </summary>
         IDependencyResolver CreateResolver();
    }
}
