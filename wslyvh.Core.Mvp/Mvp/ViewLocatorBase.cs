using Microsoft.Practices.ServiceLocation;
using System.Web.UI;
using wslyvh.Core.Web.Mvp.Interfaces;

namespace wslyvh.Core.Web.Mvp
{
    public abstract class ViewLocatorBase<TPresenter> : UserControl, IView
        where TPresenter : IPresenter
    {
        private TPresenter Presenter { get; set; }

        protected ViewLocatorBase()
        {
            this.Presenter = ServiceLocator.Current.GetInstance<TPresenter>();
            this.Presenter.SetView(this);
        }
    }
}
