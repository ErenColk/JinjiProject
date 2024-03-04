using JinjiProject.Dtos.Categories;
using JinjiProject.Dtos.Materials;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JinjiProject.BusinessLayer.Helpers.SelectItemProducts
{
    public class MaterialItems
    {
        public static async Task<List<SelectListItem>> GetMaterial(List<ListMaterialDto> listMaterialDto)
        {
            var materialList = listMaterialDto.Select(x => new SelectListItem()
            {
                Value = x.Id.ToString(),
                Text = x.Name
            }).ToList();

            return materialList;
        }
    }
}
