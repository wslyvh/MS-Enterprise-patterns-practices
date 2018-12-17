namespace wslyvh.Core.Web.Mvp.Interfaces
{
    public interface IPresenter
    {
        void SubscribeViewToEvents();
        void SetView(IView view);
    }
}