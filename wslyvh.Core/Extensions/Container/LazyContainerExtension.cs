using System;
using Microsoft.Practices.ObjectBuilder2;
using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.ObjectBuilder;
using wslyvh.Core.Extensions.Builder;
using wslyvh.Core.Extensions.Policy;

namespace wslyvh.Core.Extensions.Container
{
    public class LazyContainerExtension : UnityContainerExtension
    {
        protected override void Initialize()
        {
            Context.Strategies.AddNew<LazyResolutionStrategy>(UnityBuildStage.TypeMapping);
            Context.Policies.Set<IBuildPlanPolicy>(new LazyResolveBuildPlanPolicy(), typeof(Lazy<>));
        }
    }
}
