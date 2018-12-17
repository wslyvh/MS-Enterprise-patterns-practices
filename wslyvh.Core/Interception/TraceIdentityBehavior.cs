using System;
using System.Diagnostics;
using System.Threading;
using Microsoft.Practices.Unity.InterceptionExtension;
using wslyvh.Core.Interfaces.Diagnostics;

namespace wslyvh.Core.Interception
{
    using System.Security.Principal;

    public class TraceIdentityBehavior : BaseInterceptionBehavior
    {
        private readonly ILogger _logger;

        public TraceIdentityBehavior(ILogger logger)
        {
            Guard.ArgumentIsNotNull(logger, "logger");

            _logger = logger;
        }

        public override IMethodReturn Invoke(IMethodInvocation input, GetNextInterceptionBehaviorDelegate getNext)
        {
            Guard.ArgumentIsNotNull(input, "input");
            Guard.ArgumentIsNotNull(getNext, "getNext");
            
            var identity = WindowsIdentity.GetCurrent() ?? Thread.CurrentPrincipal.Identity as WindowsIdentity;

            _logger.Write(string.Format("Thread Identity before Invocation. Name: {0} Authentication: {1} Authenticated: {2} ImpersonationLevel: {3}",
                identity.Name, identity.AuthenticationType, identity.IsAuthenticated, identity.ImpersonationLevel),
                TraceEventType.Information);

            var nextMethod = getNext().Invoke(input, getNext);

            identity = WindowsIdentity.GetCurrent() ?? Thread.CurrentPrincipal.Identity as WindowsIdentity;

            _logger.Write(string.Format("Thread Identity after Invocation. Name: {0} Authentication: {1} Authenticated: {2} ImpersonationLevel: {3}",
                identity.Name, identity.AuthenticationType, identity.IsAuthenticated, identity.ImpersonationLevel),
                TraceEventType.Information);

            return nextMethod;
        }
    }
}
