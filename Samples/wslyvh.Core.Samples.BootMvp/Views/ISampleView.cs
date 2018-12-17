using System;
using wslyvh.Core.Interfaces;
using wslyvh.Core.Web.Mvp.Interfaces;

namespace wslyvh.Core.Samples.BootMvp.Views
{
    public interface ISampleView : IView
    {
        string Message { get; set; }

        event EventHandler GetMessage;
        event EventHandler<EventArgs<string>> PostMessage;
    }
}