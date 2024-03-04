using JinjiProject.BusinessLayer.Managers.Abstract;
using JinjiProject.Core.Enums;
using JinjiProject.Dtos.Brands;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JinjiProject.BusinessLayer.Helpers.SelectItemProducts
{
    public class BrandItems
    {


        public static async Task<List<SelectListItem>> GetBrands(List<ListBrandDto> listBrandDto)
        {

            var brandList = listBrandDto.Select(x => new SelectListItem()
            {
                Value = x.Id.ToString(),
                Text = x.Name
            }).ToList();

            return brandList;
        }

    }
}
