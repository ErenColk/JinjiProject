using JinjiProject.Dtos.Brands;
using JinjiProject.Dtos.Genres;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JinjiProject.BusinessLayer.Helpers.SelectItemProducts
{
    public class GenreItems
    {
        public static async Task<List<SelectListItem>> GetGenres(List<ListGenreDto> listGenreDto)
        {

            var genreList = listGenreDto.Select(x => new SelectListItem()
            {
                Value = x.Id.ToString(),
                Text = x.Name
            }).ToList();

            return genreList;
        }
    }
}
