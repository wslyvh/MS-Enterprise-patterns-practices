using System;
using wslyvh.Core.Extensions;
using wslyvh.Core.Interfaces;
using wslyvh.Core.Samples.BootMvp.Presenters;
using wslyvh.Core.Web.Mvp;

namespace wslyvh.Core.Samples.BootMvp.Views
{
    public partial class SampleView : ViewLocatorBase<ISamplePresenter>, ISampleView
    {
        public string Message
        {
            get { return GetMessageResult.Text; }
            set { GetMessageResult.Text = value; }
        }

        public event EventHandler GetMessage;
        public event EventHandler<EventArgs<string>> PostMessage;

        protected void PostMessageButton_Click(object sender, EventArgs e)
        {
            if (PostMessage == null)
                return;

            var args = PostMessage.CreateArgs(PostMessageTextBox.Text);
            PostMessage(this, args);
        }

        protected void GetMessageButton_Click(object sender, EventArgs e)
        {
            if (GetMessage == null)
                return;

            GetMessage(this, EventArgs.Empty);
        }
    }
}