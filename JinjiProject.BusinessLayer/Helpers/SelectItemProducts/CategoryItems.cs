using JinjiProject.Core.Entities.Concrete;
using JinjiProject.Dtos.Brands;
using JinjiProject.Dtos.Categories;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JinjiProject.BusinessLayer.Helpers.SelectItemProducts
{
    public class CategoryItems
    {
        public static async Task<List<SelectListItem>> GetCategory(List<ListCategoryDto> listCategoryDto)
        {
            var categoryList = listCategoryDto.Select(x => new SelectListItem()
            {
                Value = x.Id.ToString(),
                Text = x.Name
            }).ToList();

            return categoryList;
        }
    }
}
