using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using Microsoft.Practices.Unity;
using wslyvh.Core.Web.Context;
using wslyvh.Core.Web.Mvp.Interfaces;

namespace wslyvh.Core.Web.HttpModules
{
    public class UnityHttpModule : IHttpModule
    {
        #region IHttpModule Members
        private HttpApplication _context;

        public void Init(HttpApplication context)
        {
            _context = context;
            _context.PreRequestHandlerExecute += OnPreRequestHandlerExecute;
        }

        public void Dispose() { }
        #endregion

        private void OnPreRequestHandlerExecute(object sender, EventArgs e)
        {
            var currentHandler = HttpContext.Current.Handler;
            var container = CoreContext.Current.Items[ContextKeys.Container] as IUnityContainer;
            container.BuildUp(currentHandler.GetType(), currentHandler);

            var currentPage = HttpContext.Current.Handler as Page;
            if (currentPage != null)
                currentPage.InitComplete += OnPageInitComplete;
        }

        private void OnPageInitComplete(object sender, EventArgs e)
        {
            var currentPage = (Page)sender;
            var container = CoreContext.Current.Items[ContextKeys.Container] as IUnityContainer;

            foreach (var c in GetControlTree(currentPage).OfType<IView>())
                container.BuildUp(c.GetType(), c);

            _context.PreRequestHandlerExecute -= OnPreRequestHandlerExecute;
        }

        private IEnumerable<Control> GetControlTree(Control root)
        {
            foreach (Control child in root.Controls)
            {
                yield return child;

                foreach (var c in GetControlTree(child))
                    yield return c;
            }
        }
    }
}
