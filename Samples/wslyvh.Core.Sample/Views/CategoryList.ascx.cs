using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using wslyvh.Core.Sample.Business.Dto;
using wslyvh.Core.Sample.Presenters.Interfaces;
using wslyvh.Core.Sample.Views.Interfaces;
using wslyvh.Core.Web.Mvp;

namespace wslyvh.Core.Sample.Views
{
    public partial class CategoryList : ViewLocatorBase<ICategoryListPresenter>, ICategoryListView
    {
        public List<CategoryDto> Categories { get; set; }

        public event EventHandler GetCategories;

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            if (GetCategories != null)
                GetCategories(this, EventArgs.Empty);
        }

        protected override void OnPreRender(EventArgs e)
        {
            base.OnPreRender(e);

            CategoriesListView.DataSource = Categories;
            CategoriesListView.DataBind();
        }

        protected void AddItemClick(object sender, EventArgs e)
        {
            CategoriesListView.InsertItemPosition = InsertItemPosition.FirstItem;
        }
    }
}