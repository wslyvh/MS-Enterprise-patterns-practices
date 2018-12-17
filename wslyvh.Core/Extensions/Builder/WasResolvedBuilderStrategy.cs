using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Practices.ObjectBuilder2;

namespace wslyvh.Core.Extensions.Builder
{
    public class WasResolvedBuilderStrategy : BuilderStrategy
    {
        private IList<NamedTypeBuildKey> buildKeys = new List<NamedTypeBuildKey>();

        public override void PreBuildUp(IBuilderContext context)
        {
            Guard.ArgumentIsNotNull(context, "context");

            buildKeys.Add((NamedTypeBuildKey)context.BuildKey);
        }

        public bool WasResolved<T>()
        {
            return WasResolved<T>(null);
        }

        public bool WasResolved<T>(string name)
        {
            var buildKey = (buildKeys.FirstOrDefault(k =>
            typeof(T).IsAssignableFrom(k.Type) && k.Name == name));

            return buildKey.Type != null;
        }
    }
}
