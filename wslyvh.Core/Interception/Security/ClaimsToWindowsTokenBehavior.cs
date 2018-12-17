using System;
using System.Linq;
using System.Security.Principal;
using System.Threading;
using Microsoft.IdentityModel.Claims;
using Microsoft.IdentityModel.WindowsTokenService;
using Microsoft.Practices.ObjectBuilder2;
using Microsoft.Practices.Unity.InterceptionExtension;

namespace wslyvh.Core.Interception.Security
{
    public class ClaimsToWindowsTokenBehavior : BaseInterceptionBehavior
    {
        public override IMethodReturn Invoke(IMethodInvocation input, GetNextInterceptionBehaviorDelegate getNext)
        {
            Guard.ArgumentIsNotNull(input, "input");
            Guard.ArgumentIsNotNull(getNext, "getNext");

            IMethodReturn nextMethod;
            using (RetrieveWindowsIdentityForCurrentUserBasedOnClaim().Impersonate())
            {
                nextMethod = getNext().Invoke(input, getNext);
            }

            return nextMethod;
        }

        protected static WindowsIdentity RetrieveWindowsIdentityForCurrentUserBasedOnClaim()
        {
            IClaimsIdentity identity = Thread.CurrentPrincipal.Identity as ClaimsIdentity;
            if (identity == null)
                throw new InvalidOperationException("Current Thread Identity is not a valid Claims Identity.");

            string upn = null;
            var correctClaimType = identity.Claims.Where(claim => StringComparer.Ordinal.Equals(ClaimTypes.Upn, claim.ClaimType));
            correctClaimType.ForEach(c => upn = c.Value);

            if (string.IsNullOrEmpty(upn))
                throw new InvalidOperationException("Current Thread Identity is not a valid Windows Identity.");

            WindowsIdentity windowsIdentity;
            using (WindowsIdentity.Impersonate(IntPtr.Zero))
                windowsIdentity = S4UClient.UpnLogon(upn);

            return windowsIdentity;
        }
    }
}
