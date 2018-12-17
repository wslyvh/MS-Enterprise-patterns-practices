using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using wslyvh.Core.Mvp;
using wslyvh.Core.Sample.Business.Dto;
using wslyvh.Core.Sample.Business.Models.Interfaces;

namespace wslyvh.Core.Sample.Business.Models
{
    public class CategoryListModel : Model, ICategoryListModel
    {
        public List<CategoryDto> GetCategories()
        {
            return new List<CategoryDto> { new CategoryDto(){
                Id = 1,
                Name = "Message #1"
            }};

        }
    }
}
