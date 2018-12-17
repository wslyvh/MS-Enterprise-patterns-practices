namespace wslyvh.Core.Samples.BootMvp.Models
{
    using wslyvh.Core.Web.Mvp.Interfaces;

    public interface ISampleModel : IModel
    {
        string GetMessage();
        void PostMessage(string message);
    }
}