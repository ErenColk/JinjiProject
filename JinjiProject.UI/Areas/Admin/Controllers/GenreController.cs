using AutoMapper;
using JinjiProject.BusinessLayer.Helpers.SelectItemProducts;
using JinjiProject.BusinessLayer.Managers.Abstract;
using JinjiProject.Core.Enums;
using JinjiProject.Dtos.Brands;
using JinjiProject.Dtos.Genres;
using Microsoft.AspNetCore.Mvc;

namespace JinjiProject.UI.Areas.Admin.Controllers
{
    public class GenreController : AdminBaseController
    {
        private readonly IGenreService _genreService;
        private readonly IMapper _mapper;
        private readonly ICategoryService _categoryService;

        public GenreController(IGenreService genreService, IMapper mapper, ICategoryService categoryService)
        {
            _genreService = genreService;
            _mapper = mapper;
            _categoryService = categoryService;
        }

        [HttpGet]
        public async Task<IActionResult> GenreList(bool showWarning = true)
        {
            var genreListResult = await _genreService.GetAllByExpression(genre => genre.Status == Status.Active || genre.Status == Status.Modified);
            var genreList = _mapper.Map<List<ListGenreDto>>(genreListResult.Data);

            if ((genreList.Count <= 0 || genreList == null) && showWarning)
            {
                NotifyError(genreListResult.Message);
            }
            else if (showWarning)
            {
                NotifySuccess(genreListResult.Message);
            }

            return View(genreList);
        }

        [HttpGet]
        public async Task<IActionResult> CreateGenre()
        {
            var listCategoryDto = await _categoryService.GetAllByExpression(category => category.Status != Status.Deleted);
            ViewBag.Categories = await CategoryItems.GetCategory(listCategoryDto.Data);
            return View();
        }
    }
}
