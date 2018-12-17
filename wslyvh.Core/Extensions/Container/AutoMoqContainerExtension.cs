using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.ObjectBuilder;
using wslyvh.Core.Extensions.Builder;

namespace wslyvh.Core.Extensions.Container
{
    public class AutoMoqContainerExtension : UnityContainerExtension
    {
        protected override void Initialize()
        {
            var strategy = new MoqBuilderStrategy(this.Container);

            this.Context.Strategies.Add(strategy, UnityBuildStage.PreCreation);
        }
    }
}
