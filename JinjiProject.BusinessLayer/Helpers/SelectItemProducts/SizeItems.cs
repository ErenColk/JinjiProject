using JinjiProject.Core.Enums;
using JinjiProject.Dtos.Brands;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JinjiProject.BusinessLayer.Helpers.SelectItemProducts
{
    public class SizeItems
    {
        public static async Task<List<SelectListItem>> GetSize()
        {

            var enumValues = Enum.GetValues(typeof(Size));
            var selectList = new List<SelectListItem>();

            foreach (var value in enumValues)
            {
                selectList.Add(new SelectListItem
                {
                    Value = ((int)value).ToString(),
                    Text = Enum.GetName(typeof(Size), value)
                });
            }

            return selectList;
        }
    }


}


