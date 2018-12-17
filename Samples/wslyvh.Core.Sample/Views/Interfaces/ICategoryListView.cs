using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using wslyvh.Core.Sample.Business.Dto;
using wslyvh.Core.Web.Mvp.Interfaces;

namespace wslyvh.Core.Sample.Views.Interfaces
{
    public interface ICategoryListView : IView
    {
        List<CategoryDto> Categories { get; set; }

        event EventHandler GetCategories;
    }
}