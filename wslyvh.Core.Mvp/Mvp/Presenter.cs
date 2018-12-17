using wslyvh.Core.Web.Mvp.Interfaces;

namespace wslyvh.Core.Web.Mvp
{
    public abstract class Presenter<TView, TModel> : IPresenter
        where TView : IView
        where TModel : IModel
    {
        protected TView View { get; private set; }
        protected TModel Model { get; private set; }

        public abstract void SubscribeViewToEvents();

        protected Presenter(TModel model)
        {
            this.Model = model;
        }
        
        public void SetView(IView view)
        {
            this.View = (TView) view;
            this.SubscribeViewToEvents();
        }
    }
}
