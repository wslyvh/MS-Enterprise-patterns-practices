using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using wslyvh.Core.Interfaces.Boot;

namespace wslyvh.Core.Boot
{
    public abstract class BootstrapperTask : IBootstrapperTask
    {
        public abstract void Execute();
    }
}
