using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using wslyvh.Core.Sample.Business.Models.Interfaces;
using wslyvh.Core.Sample.Presenters.Interfaces;
using wslyvh.Core.Sample.Views.Interfaces;
using wslyvh.Core.Web.Mvp;

namespace wslyvh.Core.Sample.Presenters
{
    public class CategoryListPresenter : Presenter<ICategoryListView, ICategoryListModel>, ICategoryListPresenter
    {
        public CategoryListPresenter(ICategoryListModel model)
            : base(model)
        {
        }

        public override void SubscribeViewToEvents()
        {
            View.GetCategories += View_GetCategories;
        }

        private void View_GetCategories(object sender, EventArgs e)
        {
            View.Categories = Model.GetCategories();
        }
    }
}