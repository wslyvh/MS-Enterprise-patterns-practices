using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.ObjectBuilder;
using wslyvh.Core.Extensions.Builder;

namespace wslyvh.Core.Extensions.Container
{
    public class WasResolvedContainerExtension : UnityContainerExtension
    {
        private WasResolvedBuilderStrategy strategy;

        protected override void Initialize()
        {
            this.strategy = new WasResolvedBuilderStrategy();

            this.Context.Strategies.Add(this.strategy, UnityBuildStage.Creation);
        }

        public bool WasResolved<T>()
        {
            return this.WasResolved<T>(null);
        }

        public bool WasResolved<T>(string name)
        {
            return this.strategy.WasResolved<T>(name);
        }
    }
}
