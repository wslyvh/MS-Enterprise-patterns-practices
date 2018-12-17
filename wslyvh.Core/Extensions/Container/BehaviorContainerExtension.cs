using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.InterceptionExtension;
using wslyvh.Core.Interception.Rules;
using wslyvh.Core.Interfaces.Interception;
using unity = Microsoft.Practices.Unity.InterceptionExtension;

namespace wslyvh.Core.Extensions.Container
{
    using Microsoft.Practices.ObjectBuilder2;

    public class BehaviorContainerExtension : UnityContainerExtension
    {
        private IInstanceInterceptor _interceptor;
        private readonly IList<IInterceptionBehavior> _behaviors;
        private readonly IList<IInterceptRule> _rules;

        public BehaviorContainerExtension()
        {
            _interceptor = new TransparentProxyInterceptor();
            _behaviors = new List<IInterceptionBehavior>();
            _rules = new List<IInterceptRule>();
        }

        public BehaviorContainerExtension SetDefaultInterceptor<T>() where T : IInstanceInterceptor, new()
        {
            _interceptor = new T();
            return this;
        }

        public BehaviorContainerExtension AddMatchingRule(IInterceptRule rule)
        {
            Guard.ArgumentIsNotNull(rule, "rule");

            _rules.Add(rule);
            return this;
        }

        public BehaviorContainerExtension AddNewMatchingRule<T>() where T : IInterceptRule, new()
        {
            _rules.Add(new T());
            return this;
        }

        public BehaviorContainerExtension AddBehavior(IInterceptionBehavior behavior)
        {
            Guard.ArgumentIsNotNull(behavior, "behavior");

            _behaviors.Add(behavior);
            return this;
        }

        public BehaviorContainerExtension AddNewBehavior<T>() where T : IInterceptionBehavior, new()
        {
            _behaviors.Add(new T());
            return this;
        }

        protected override void Initialize()
        {
            AddInterceptionExtensionIfNotExists();

            Context.Registering += (sender, e) => SetInterceptionBehaviorFor(e.TypeFrom, e.TypeTo ?? e.TypeFrom, e.Name);
        }

        private void AddInterceptionExtensionIfNotExists()
        {
            if (Container.Configure<unity.Interception>() == null)
                Container.AddNewExtension<unity.Interception>();
        }

        private void SetInterceptionBehaviorFor(Type typeToIntercept, Type typeOfInstance, string name)
        {
            if (!AllMatchingRulesApply(typeToIntercept, typeOfInstance))
                return;

            if (!TypeIsInterface(typeToIntercept)) 
                return;

            if (_interceptor.CanIntercept(typeOfInstance))
                Container.Configure<unity.Interception>().SetDefaultInterceptorFor(typeOfInstance, _interceptor);

            else if (_interceptor.CanIntercept(typeToIntercept))
                Container.Configure<unity.Interception>().SetDefaultInterceptorFor(typeToIntercept, _interceptor);

            _behaviors.ForEach(b =>
                {
                    var interceptionBehavior = new InterceptionBehavior(b);
                    interceptionBehavior.AddPolicies(typeToIntercept, typeOfInstance, name, Context.Policies);
                });
        }

        private bool AllMatchingRulesApply(Type typeToIntercept, Type typeOfInstance)
        {
            return _rules.All(rule => rule.Matches(typeToIntercept, typeOfInstance));
        }

        private bool TypeIsInterface(Type typeToIntercept)
        {
            return typeToIntercept.IsInterface;
        }
    }
}
