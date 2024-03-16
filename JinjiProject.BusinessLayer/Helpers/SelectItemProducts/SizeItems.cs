using JinjiProject.Core.Enums;
using JinjiProject.Dtos.Brands;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace JinjiProject.BusinessLayer.Helpers.SelectItemProducts
{
    public class SizeItems
    {
        public static async Task<List<SelectListItem>> GetSize()
        {
            return Enum.GetValues(typeof(Size)).Cast<Size>()
      .Select(x => new SelectListItem
      {
          Text = x.ToString(),
          Value = ((int)x).ToString()
      }).ToList();

        }
    }


}


