using System;
using System.Web;
using Microsoft.Practices.Unity;

namespace wslyvh.Core.Web.Boot.Lifetime
{
    public class PerRequestLifetimeManager : LifetimeManager
    {
        private readonly Guid _key;

        public PerRequestLifetimeManager()
        {
            _key = Guid.NewGuid();
        }

        public override object GetValue()
        {
            return HttpContext.Current.Items[_key];
        }

        public override void SetValue(object newValue)
        {
            HttpContext.Current.Items[_key] = newValue;
        }

        public override void RemoveValue()
        {
            HttpContext.Current.Items.Remove(_key);
        }
    }
}
