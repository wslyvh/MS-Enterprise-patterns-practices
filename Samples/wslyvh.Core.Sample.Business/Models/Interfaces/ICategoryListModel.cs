using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using wslyvh.Core.Sample.Business.Dto;
using wslyvh.Core.Web.Mvp.Interfaces;

namespace wslyvh.Core.Sample.Business.Models.Interfaces
{
    public interface ICategoryListModel : IModel
    {
        List<CategoryDto> GetCategories();
    }
}
