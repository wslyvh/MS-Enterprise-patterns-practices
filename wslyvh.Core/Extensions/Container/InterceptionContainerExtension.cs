using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.InterceptionExtension;
using wslyvh.Core.Interfaces.Interception;
using unity = Microsoft.Practices.Unity.InterceptionExtension;

namespace wslyvh.Core.Extensions.Container
{
    public class InterceptionContainerExtension : UnityContainerExtension
    {
        private IInstanceInterceptor _interceptor;
        private readonly IList<IInterceptRule> _rules;

        public InterceptionContainerExtension()
        {
            _interceptor = new TransparentProxyInterceptor();
            _rules = new List<IInterceptRule>();
        }

        public InterceptionContainerExtension AddMatchingRule(IInterceptRule rule)
        {
            Guard.ArgumentIsNotNull(rule, "rule");

            _rules.Add(rule);
            return this;
        }

        public InterceptionContainerExtension AddNewMatchingRule<T>() where T : IInterceptRule, new()
        {
            _rules.Add(new T());
            return this;
        }

        public InterceptionContainerExtension SetDefaultInterceptor<T>() where T : IInstanceInterceptor, new()
        {
            _interceptor = new T();
            return this;
        }

        protected override void Initialize()
        {
            AddInterceptionExtensionIfNotExists();

            Context.Registering += (sender, e) => this.SetInterceptorFor(e.TypeFrom, e.TypeTo ?? e.TypeFrom);
        }

        private void AddInterceptionExtensionIfNotExists()
        {
            if (Container.Configure<unity.Interception>() == null)
                Container.AddNewExtension<unity.Interception>();
        }

        private void SetInterceptorFor(Type typeToIntercept, Type typeOfInstance)
        {
            if (!AllMatchingRulesApply(typeToIntercept, typeOfInstance))
                return;

            if (_interceptor.CanIntercept(typeOfInstance))
                Container.Configure<unity.Interception>().SetDefaultInterceptorFor(typeOfInstance, _interceptor);

            else if (_interceptor.CanIntercept(typeToIntercept))
                Container.Configure<unity.Interception>().SetDefaultInterceptorFor(typeToIntercept, _interceptor);
        }

        private bool AllMatchingRulesApply(Type typeToIntercept, Type typeOfInstance)
        {
            return _rules.All(rule => rule.Matches(typeToIntercept, typeOfInstance));
        }
    }
}
