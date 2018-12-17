using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading;

namespace wslyvh.Core
{
    public class ApplicationContext
    {
        private HybridDictionary _items = new HybridDictionary();
        
        public IDictionary Items
        {
            get { return _items; }
        }

        protected ApplicationContext()
        {
        }
        #region Current
        public static ApplicationContext Current
        {
            get
            {
                var context = new ApplicationContext();

                //if (context == null)
                //{
                //    context = new ApplicationContext();
                //    //SaveContext(context, ContextKeys.Store);
                //}

                return context;
            }
        }

        //protected static ApplicationContext GetContext()
        //{
        //    //var httpContext = HttpContext.Current;

        //    //if (httpContext != null)
        //    //    return httpContext.Items[ContextKeys.Store] as ApplicationContext;

        //    //return Thread.GetData(GetSlot(ContextKeys.Store)) as ApplicationContext;
        //}

        //protected static void SaveContext(ApplicationContext context, string dataKey)
        //{
        //    var httpContext = HttpContext.Current;

        //    if (httpContext != null)
        //        httpContext.Items[dataKey] = context;
        //    else
        //        Thread.SetData(GetSlot(dataKey), context);
        //}

        //private static LocalDataStoreSlot GetSlot(string dataKey)
        //{
        //    return Thread.GetNamedDataSlot(dataKey);
        //}
        #endregion

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
