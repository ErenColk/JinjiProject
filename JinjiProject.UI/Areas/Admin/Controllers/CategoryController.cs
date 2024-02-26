using AutoMapper;
using JinjiProject.BusinessLayer.Managers.Abstract;
using JinjiProject.Dtos.Categories;
using Microsoft.AspNetCore.Mvc;

namespace JinjiProject.UI.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CategoryController : Controller
    {
        private readonly ICategoryService _categoryService;
        private readonly IMapper _mapper;

        public CategoryController(ICategoryService categoryService,IMapper mapper )
        {
            _categoryService = categoryService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> CategoryList()
        {
            var categoryListResult = await _categoryService.GetAllCategory();
            var categoryList = _mapper.Map<List<ListCategoryDto>>(categoryListResult.Data);
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
            var createResult = await _categoryService.CreateCategoryAsync(createCategoryDto); ;

            return RedirectToAction(nameof(CategoryList));
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

    }
}
