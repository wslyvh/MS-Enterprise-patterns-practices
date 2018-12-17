using System;
using wslyvh.Core.Interfaces;
using wslyvh.Core.Samples.BootMvp.Models;
using wslyvh.Core.Samples.BootMvp.Views;
using wslyvh.Core.Web.Mvp;

namespace wslyvh.Core.Samples.BootMvp.Presenters
{
    public class SamplePresenter : Presenter<ISampleView, ISampleModel>, ISamplePresenter
    {
        public SamplePresenter(ISampleModel model) : base(model)
        {
        }
        
        public override void SubscribeViewToEvents()
        {
            View.GetMessage += View_GetMessage;
            View.PostMessage += View_PostMessage;
        }

        private void View_GetMessage(object sender, EventArgs e)
        {
            View.Message = Model.GetMessage();
        }

        private void View_PostMessage(object sender, EventArgs<string> e)
        {
            Model.PostMessage(e.Parameter);
        }
    }
}