using AutoMapper;
using JinjiProject.BusinessLayer.Helpers.SelectItemProducts;
using JinjiProject.BusinessLayer.Managers.Abstract;
using JinjiProject.BusinessLayer.Validator.BrandValidations;
using JinjiProject.BusinessLayer.Validator.GenreValidations;
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

        [HttpPost]
        public async Task<IActionResult> CreateGenre(CreateGenreDto createGenreDto)
        {
            CreateGenreValidator validations = new CreateGenreValidator();
            var result = validations.Validate(createGenreDto);

            if (result.IsValid)
            {
                var createGenreResult = await _genreService.CreateGenreAsync(createGenreDto); ;
                if (createGenreResult.IsSuccess)
                {
                    NotifySuccess(createGenreResult.Message);
                }
                else
                {
                    NotifyError(createGenreResult.Message);

                }
                return RedirectToAction(nameof(GenreList), new { showWarning = false });
            }

            foreach (var item in result.Errors)
            {
                if (item.ErrorCode == "1")
                {
                    ViewData["NameError"] += item.ErrorMessage + "\n";
                }
                else
                {
                    ViewData["DescriptionError"] += item.ErrorMessage + "\n";

                }
            }

            return View(createGenreDto);
        }

        [HttpGet]
        public async Task<IActionResult> UpdateGenre(int id)
        {
            var updateGenreResult = await _genreService.GetGenreById(id);
            if (updateGenreResult.IsSuccess)
            {
                var listCategoryDto = await _categoryService.GetAllByExpression(category => category.Status != Status.Deleted);
                ViewBag.Categories = await CategoryItems.GetCategory(listCategoryDto.Data);

                UpdateGenreDto updateGenre = _mapper.Map<UpdateGenreDto>(updateGenreResult.Data);
                return View(updateGenre);

            }
            else
            {
                UpdateGenreDto updateGenre = _mapper.Map<UpdateGenreDto>(updateGenreResult.Data);
                NotifyError(updateGenreResult.Message);
                return RedirectToAction(nameof(GenreList), new { showWarning = false });
            }

        }

        [HttpPost]
        public async Task<IActionResult> UpdateGenre(UpdateGenreDto updateGenreDto)
        {
            UpdateGenreValidator updateGenreValidator = new UpdateGenreValidator();
            var result = updateGenreValidator.Validate(updateGenreDto);

            if (result.IsValid)
            {
                var updateGenreResult = await _genreService.UpdateGenreAsync(updateGenreDto);

                if (updateGenreResult.IsSuccess)
                {

                    NotifySuccess(updateGenreResult.Message);

                }
                else
                {
                    NotifyError(updateGenreResult.Message);
                }

                return RedirectToAction(nameof(GenreList), new { showWarning = false });
            }

            foreach (var item in result.Errors)
            {
                if (item.ErrorCode == "1")
                {
                    ViewData["NameError"] += item.ErrorMessage + "\n";
                }
                else
                {
                    ViewData["DescriptionError"] += item.ErrorMessage + "\n";

                }
            }

            return View(updateGenreDto);
        }

        [HttpGet]
        public async Task<IActionResult> SoftDelete(int id)
        {

            var softDeleteResult = await _genreService.SoftDeleteGenreAsync(id);

            if (softDeleteResult.IsSuccess)
            {
                NotifySuccess(softDeleteResult.Message);


                return RedirectToAction(nameof(GenreList), new { showWarning = false });
            }
            else
            {
                if (softDeleteResult.Data == null)
                {
                    NotifyError(softDeleteResult.Message);

                    return RedirectToAction(nameof(GenreList), new { showWarning = false });
                }
                NotifyError(softDeleteResult.Message);

                return RedirectToAction(nameof(GenreList), new { showWarning = false });
            }

        }

        [HttpGet]
        public async Task<IActionResult> HardDelete(int id)
        {

            var hardDeleteResult = await _genreService.HardDeleteGenreAsync(id);

            if (hardDeleteResult.IsSuccess)
            {
                NotifySuccess(hardDeleteResult.Message);


                return RedirectToAction(nameof(DeletedGenreList), new { showWarning = false });
            }
            else
            {
                if (hardDeleteResult.Data == null)
                {
                    NotifyError(hardDeleteResult.Message);

                    return RedirectToAction(nameof(DeletedGenreList), new { showWarning = false });
                }
                NotifyError(hardDeleteResult.Message);

                return RedirectToAction(nameof(DeletedGenreList), new { showWarning = false });
            }
        }


        [HttpGet]
        public async Task<IActionResult> DeletedGenreList(bool showWarning = true)
        {

            var deletedGenre = await _genreService.GetAllByExpression(x => x.Status == Status.Deleted);
            List<DeletedGenreListDto> deletedGenreList = _mapper.Map<List<DeletedGenreListDto>>(deletedGenre.Data);

            if ((deletedGenreList.Count <= 0 || deletedGenreList == null) && showWarning)
            {
                NotifyError("Silinen Kategori Türleri Listesi Boş");
            }
            else if (showWarning)
            {
                NotifySuccess("Silinen Kategori Türleri Listelendi");

            }

            return View(deletedGenreList);

        }

        [HttpGet]
        public async Task<IActionResult> AddAgainGenre(int id)
        {
            var genreToAdded = await _genreService.GetGenreById(id);

            if (genreToAdded.Data == null)
            {
                NotifyError(genreToAdded.Message);
                return RedirectToAction(nameof(DeletedGenreList), new { showWarning = false });
            }
            else
            {

                genreToAdded.Data.Status = Status.Active;
                UpdateGenreDto updatedToGenre = _mapper.Map<UpdateGenreDto>(genreToAdded.Data);

                var genreToUpdated = await _genreService.UpdateGenreAsync(updatedToGenre);
                NotifySuccess("Kategori türü yeniden eklendi.");

                return RedirectToAction(nameof(DeletedGenreList), new { showWarning = false });
            }
        }


        [HttpGet]
        public async Task<DetailGenreDto> GetDetailGenre(int genreid)
        {

            var genre = await _genreService.GetGenreById(genreid);
            var genreResult = _mapper.Map<DetailGenreDto>(genre.Data);

            return genreResult;
        }

        [HttpGet]
        public async Task<GetGenreDto> GetGenre(int genreid)
        {

            var genreResult = await _genreService.GetGenreById(genreid);

            return genreResult.Data;
        }

    }
}
