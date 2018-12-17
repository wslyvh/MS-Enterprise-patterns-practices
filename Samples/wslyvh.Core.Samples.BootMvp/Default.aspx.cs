using Microsoft.Practices.ServiceLocation;
using System;
using wslyvh.Core.Samples.BootMvp.Models;

namespace wslyvh.Core.Samples.BootMvp
{
    public partial class Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            var model = ServiceLocator.Current.GetInstance<ISampleModel>();
            var msg = model.GetMessage();
        }
    }
}