using AutoMapper;
using JinjiProject.BusinessLayer.Helpers.SelectItemProducts;
using JinjiProject.BusinessLayer.Managers.Abstract;
using JinjiProject.BusinessLayer.Validator.BrandValidations;
using JinjiProject.BusinessLayer.Validator.GenreValidations;
using JinjiProject.Core.Entities.Concrete;
using JinjiProject.Core.Enums;
using JinjiProject.Dtos.Brands;
using JinjiProject.Dtos.Categories;
using JinjiProject.Dtos.Genres;
using JinjiProject.Dtos.Materials;
using JinjiProject.Dtos.Products;
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

            var listCategoryDto = await _categoryService.GetAllByExpression(category => category.Status != Status.Deleted);
            if (!listCategoryDto.IsSuccess)
            {
                NotifyError("Öncelikle gerekli diğer özellikler eklenmelidir!");
                return RedirectToAction("Index", "Home");
            }
            ViewBag.Categories = await CategoryItems.GetCategory(listCategoryDto.Data);

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

        [HttpPost]
        public async Task<IActionResult> GetGenresByGivenValues(ListGenreDto listGenreDto)
        {
            string category = null;
            if (listGenreDto.CategoryId != 0)
                category = listGenreDto.CategoryId.ToString();

            var genreListResponse = await _genreService.GetGenreBySearchValues(listGenreDto.Name, category, listGenreDto.CreatedDate.ToString());

            if (category == null && listGenreDto.CreatedDate.Year == 1 && listGenreDto.Name == null)
                return RedirectToAction("GenreList");

            var listCategoryDto = await _categoryService.GetAllByExpression(category => category.Status != Status.Deleted);
            if (!listCategoryDto.IsSuccess)
            {
                NotifyError("Öncelikle gerekli diğer özellikler eklenmelidir!");
                return RedirectToAction("Index", "Home");
            }
            ViewBag.Categories = await CategoryItems.GetCategory(listCategoryDto.Data);

            return View("GenreList", genreListResponse.Data);
        }

        [HttpGet]
        public async Task<IActionResult> CreateGenre()
        {
            var listCategoryDto = await _categoryService.GetAllByExpression(category => category.Status != Status.Deleted);
            if (listCategoryDto.Data != null)
            {
                ViewBag.Categories = await CategoryItems.GetCategory(listCategoryDto.Data);

            }
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
                else if (item.ErrorCode == "2")
                {
                    ViewData["DescriptionError"] += item.ErrorMessage + "\n";
                }
                else if (item.ErrorCode == "3")
                {
                    ViewData["ImageError"] += item.ErrorMessage + "\n";
                }
                else if (item.ErrorCode == "4")
                {
                    ViewData["CategoryError"] += item.ErrorMessage + "\n";
                }
            }
            var listCategoryDto = await _categoryService.GetAllByExpression(category => category.Status != Status.Deleted);
            if (listCategoryDto.Data != null)
            {
                ViewBag.Categories = await CategoryItems.GetCategory(listCategoryDto.Data);

            }
            return View(createGenreDto);
        }

        [HttpGet]
        public async Task<IActionResult> HomePageEditGenre()
        {
            var genreResult = await _genreService.GetAllByExpression(genre => genre.Status != Status.Deleted);
            if (genreResult.IsSuccess)
            {
                List<ListHomePageGenreDto> genreDtos = _mapper.Map<List<ListHomePageGenreDto>>(genreResult.Data.OrderBy(genre => genre.Order));
                NotifySuccess(genreResult.Message);
                return View(genreDtos);

            }
            else
            {
                NotifyError(genreResult.Message);
                return View();
            }

        }

        [HttpPost]
        public async Task<IActionResult> HomePageEditGenre([FromBody] List<ListHomePageGenreDto> listGenreDto)
        {
            List<UpdateHomePageGenreDto> genres = _mapper.Map<List<UpdateHomePageGenreDto>>(listGenreDto);

            var updateGenreResult = await _genreService.UpdateAllGenreAsync(genres);

            if (updateGenreResult.IsSuccess)
            {

                NotifySuccess(updateGenreResult.Message);
            }
            else
            {
                NotifyError(updateGenreResult.Message);
                return View(listGenreDto);
            }


            return Ok();
        }

        [HttpGet]
        public async Task<IActionResult> UpdateGenre(int id)
        {
            var updateGenreResult = await _genreService.GetGenreById(id);
            if (updateGenreResult.IsSuccess)
            {
                var listCategoryDto = await _categoryService.GetAllByExpression(category => category.Status != Status.Deleted);
                if (listCategoryDto.Data != null)
                {
                    ViewBag.Categories = await CategoryItems.GetCategory(listCategoryDto.Data);

                }

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
                else if (item.ErrorCode == "2")
                {
                    ViewData["DescriptionError"] += item.ErrorMessage + "\n";
                }
                else if (item.ErrorCode == "3")
                {
                    ViewData["ImageError"] += item.ErrorMessage + "\n";
                }
                else if (item.ErrorCode == "4")
                {
                    ViewData["CategoryError"] += item.ErrorMessage + "\n";
                }
            }

            var listCategoryDto = await _categoryService.GetAllByExpression(category => category.Status != Status.Deleted);
            if (listCategoryDto.Data != null)
            {
                ViewBag.Categories = await CategoryItems.GetCategory(listCategoryDto.Data);

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
