using Microsoft.Practices.Unity;

namespace wslyvh.Core.Boot.Unity
{
    public class UnityBootstrapperTask : BootstrapperTask
    {
        protected UnityBootstrapperTask()
        {
            
        }

        [Dependency]
        public IUnityContainer Container { get; set;}

        public override void Execute() { }
    }
}
