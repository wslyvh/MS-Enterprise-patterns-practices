using Microsoft.Practices.Unity;
using wslyvh.Core.Boot.Unity;
using wslyvh.Core.Diagnostics;
using wslyvh.Core.Interfaces.Diagnostics;
using wslyvh.Core.Sample.Business.Models;
using wslyvh.Core.Sample.Business.Models.Interfaces;
using wslyvh.Core.Sample.Presenters;
using wslyvh.Core.Sample.Presenters.Interfaces;
using wslyvh.Core.Web.Context;

namespace wslyvh.Core.Sample
{
    public class MvpBootstrapperTask : UnityBootstrapperTask
    {
        public override void Execute()
        {
            RegisterDataAccess();
            RegisterRepositories();
            RegisterModels();
            RegisterPresenters();
        }

        private void RegisterDataAccess()
        {
            
        }

        private void RegisterRepositories()
        {
            
        }

        private void RegisterModels()
        {
            if (!Container.IsRegistered<ICategoryListModel>())
                Container.RegisterType<ICategoryListModel, CategoryListModel>();
        }

        private void RegisterPresenters()
        {
            if (!Container.IsRegistered<ICategoryListPresenter>())
                Container.RegisterType<ICategoryListPresenter, CategoryListPresenter>();
        }
    }
}