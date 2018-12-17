using System;
using System.Collections;
using System.Collections.Specialized;
using System.Threading;
using System.Web;

namespace wslyvh.Core.Web.Context
{
    public class CoreContext
    {
        protected HybridDictionary _items = new HybridDictionary();
        
        #region Ctor
        protected CoreContext()
        {
        }

        protected CoreContext(HttpContext context)
        {
        }
        #endregion

        #region Current
        public static CoreContext Current
        {
            get
            {
                var context = GetContext();

                if (context == null)
                {
                    context = new CoreContext(HttpContext.Current);
                    SaveContext(context, ContextKeys.Store);
                }

                return context;
            }
        }

        protected static CoreContext GetContext()
        {
            var httpContext = HttpContext.Current;

            if (httpContext != null)
                return httpContext.Items[ContextKeys.Store] as CoreContext;

            return Thread.GetData(GetSlot(ContextKeys.Store)) as CoreContext;
        }

        protected static void SaveContext(CoreContext context, string dataKey)
        {
            var httpContext = HttpContext.Current;

            if (httpContext != null)
                httpContext.Items[dataKey] = context;
            else
                Thread.SetData(GetSlot(dataKey), context);
        }

        private static LocalDataStoreSlot GetSlot(string dataKey)
        {
            return Thread.GetNamedDataSlot(dataKey);
        }
        #endregion

        public IDictionary Items
        {
            get { return _items; }
        }

        public object this[string key]
        {
            get
            {
                return this.Items[key];
            }
            set
            {
                this.Items[key] = value;
            }
        }
    }
}
