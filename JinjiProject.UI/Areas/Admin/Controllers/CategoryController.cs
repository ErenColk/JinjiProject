using AutoMapper;
using Humanizer.Localisation;
using JinjiProject.BusinessLayer.Managers.Abstract;
using JinjiProject.BusinessLayer.Validator.CategoryValidations;
using JinjiProject.Core.Entities.Concrete;
using JinjiProject.Core.Enums;
using JinjiProject.Dtos.Categories;
using JinjiProject.UI.Controllers;
using Microsoft.AspNetCore.Mvc;

namespace JinjiProject.UI.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CategoryController : BaseController
    {
        private readonly ICategoryService _categoryService;
        private readonly IMapper _mapper;

        public CategoryController(ICategoryService categoryService, IMapper mapper)
        {
            _categoryService = categoryService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> CategoryList()
        {
            var categoryListResult = await _categoryService.GetAllByExpression(category => category.Status != Status.Deleted);
            var categoryList = _mapper.Map<List<ListCategoryDto>>(categoryListResult.Data);

            if (categoryList.Count <= 0 || categoryList == null)
            {
                NotifyError("Kategori Listesi Boş");
            }
            else
            {
                NotifySuccess("Kategoriler Listelendi");

            }

            return View(categoryList);
        }


        [HttpGet]
        public async Task<IActionResult> CreateCategory()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateCategory(CreateCategoryDto createCategoryDto)
        {

            CreateCategoryValidator validations = new CreateCategoryValidator();
            var result = validations.Validate(createCategoryDto);
            if (result.IsValid)
            {
                var createResult = await _categoryService.CreateCategoryAsync(createCategoryDto);
                NotifySuccess("Kategori başarıyla oluşturuldu");
                return RedirectToAction(nameof(CategoryList));
            }

            foreach (var item in validations)
            {

            }

            return View();



        }



        [HttpGet]
        public async Task<IActionResult> UpdateCategory(int id)
        {
            var updateCategoryResult = await _categoryService.GetCategoryById(id);
            UpdateCategoryDto updateCategory = _mapper.Map<UpdateCategoryDto>(updateCategoryResult.Data);
            return View(updateCategory);
        }


        [HttpPost]
        public async Task<IActionResult> UpdateCategory(UpdateCategoryDto updateCategoryDto)
        {
            var updateCategory = await _categoryService.UpdateCategoryAsync(updateCategoryDto);

            return RedirectToAction(nameof(CategoryList));
        }


        [HttpGet]
        public async Task<IActionResult> SoftDelete(int id)
        {

            await _categoryService.SoftDeleteCategoryAsync(id);

            return RedirectToAction(nameof(CategoryList));
        }



        [HttpGet]
        public async Task<IActionResult> HardDelete(int id)
        {

            await _categoryService.HardDeleteCategoryAsync(id);

            return RedirectToAction(nameof(CategoryList));
        }


        [HttpGet]
        public async Task<GetCategoryDto> GetCategory(int categoryid)
        {

            var categoryResult = await _categoryService.GetCategoryById(categoryid);

            return categoryResult.Data;
        }

        [HttpGet]
        public async Task<IActionResult> DeletedCategoryList()
        {

            var deletedCategory = await _categoryService.GetAllByExpression(x => x.Status == Status.Deleted);
            List<DeletedCategoryListDto> deletedCategoryList = _mapper.Map<List<DeletedCategoryListDto>>(deletedCategory.Data);
            return View(deletedCategoryList);

        }


        [HttpGet]
        public async Task<IActionResult> AddAgainCategory(int id)
        {
            var categoryToAdded = await _categoryService.GetCategoryById(id);

            if (categoryToAdded.Data == null)
            {
                return View();
            }
            else
            {

                categoryToAdded.Data.Status = Status.Active;
                UpdateCategoryDto updatedToCategory = _mapper.Map<UpdateCategoryDto>(categoryToAdded.Data);
                await _categoryService.UpdateCategoryAsync(updatedToCategory);
                return RedirectToAction("DeletedCategoryList", "Category");
            }
        }






    }
}
