using System.Web.UI;
using Microsoft.Practices.Unity;
using wslyvh.Core.Web.Context;
using wslyvh.Core.Web.Mvp.Interfaces;

namespace wslyvh.Core.Web.Mvp
{
    public abstract class ViewDependencyBase<TView, TPresenter> : UserControl, IView
        where TView : class, IView
        where TPresenter : IPresenter
    {
        [Dependency]
        public TPresenter Presenter { get; set; }

        protected ViewDependencyBase()
        {
            BuildUp();
        }

        protected void BuildUp()
        {
            var container = CoreContext.Current.Items[ContextKeys.Container] as IUnityContainer;
            container.BuildUp(this as TView);
        }
    }
}
