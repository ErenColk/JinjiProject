using AutoMapper;
using Humanizer.Localisation;
using JinjiProject.BusinessLayer.Managers.Abstract;
using JinjiProject.BusinessLayer.Validator.CategoryValidations;
using JinjiProject.Core.Entities.Concrete;
using JinjiProject.Core.Enums;
using JinjiProject.Dtos.Brands;
using JinjiProject.Dtos.Categories;
using JinjiProject.UI.Controllers;
using Microsoft.AspNetCore.Mvc;

namespace JinjiProject.UI.Areas.Admin.Controllers
{
	public class CategoryController : AdminBaseController
	{
		private readonly ICategoryService _categoryService;
		private readonly IMapper _mapper;

		public CategoryController(ICategoryService categoryService, IMapper mapper)
		{
			_categoryService = categoryService;
			_mapper = mapper;
		}

		[HttpGet]
		public async Task<IActionResult> CategoryList(bool showWarning = true)
		{
			var categoryListResult = await _categoryService.GetAllByExpression(category => category.Status != Status.Deleted);
			var categoryList = _mapper.Map<List<ListCategoryDto>>(categoryListResult.Data);

			if ((categoryList.Count <= 0 || categoryList == null) && showWarning)
			{
				NotifyError(categoryListResult.Message);
			}
			else if (showWarning)
			{
				NotifySuccess(categoryListResult.Message);
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
				var createCategoryResult = await _categoryService.CreateCategoryAsync(createCategoryDto);
				if (createCategoryResult.IsSuccess)
				{
					NotifySuccess(createCategoryResult.Message);
				}
				else
				{
					NotifyError(createCategoryResult.Message);

				}
				return RedirectToAction(nameof(CategoryList), new { showWarning = false });
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
			return View(createCategoryDto);

		}



		[HttpGet]
		public async Task<IActionResult> UpdateCategory(int id)
		{
			var updateCategoryResult = await _categoryService.GetCategoryById(id);
			if (updateCategoryResult.IsSuccess)
			{
				UpdateCategoryDto updateCategory = _mapper.Map<UpdateCategoryDto>(updateCategoryResult.Data);
				return View(updateCategory);

			}
			else
			{
				UpdateCategoryDto updateCategory = _mapper.Map<UpdateCategoryDto>(updateCategoryResult.Data);
				NotifyError(updateCategoryResult.Message);
				return RedirectToAction(nameof(CategoryList), new { showWarning = false });
			}



		}


		[HttpPost]
		public async Task<IActionResult> UpdateCategory(UpdateCategoryDto updateCategoryDto)
		{
			UpdateCategoryValidator updateCategoryValidator = new UpdateCategoryValidator();
			var result = updateCategoryValidator.Validate(updateCategoryDto);

			if (result.IsValid)
			{
				var updateCategoryResult = await _categoryService.UpdateCategoryAsync(updateCategoryDto);

				if (updateCategoryResult.IsSuccess)
				{

					NotifySuccess(updateCategoryResult.Message);

				}
				else
				{
					NotifyError(updateCategoryResult.Message);
				}

				return RedirectToAction(nameof(CategoryList), new { showWarning = false });
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
			return View(updateCategoryDto);


		}


		[HttpGet]
		public async Task<IActionResult> SoftDelete(int id)
		{

			var softDeleteResult = await _categoryService.SoftDeleteCategoryAsync(id);


			if (softDeleteResult.IsSuccess)
			{
				NotifySuccess(softDeleteResult.Message);


				return RedirectToAction(nameof(CategoryList), new { showWarning = false });
			}
			else
			{
				if (softDeleteResult.Data == null)
				{
					NotifyError(softDeleteResult.Message);

					return RedirectToAction(nameof(CategoryList), new { showWarning = false });
				}
				NotifyError(softDeleteResult.Message);

				return RedirectToAction(nameof(CategoryList), new { showWarning = false });
			}


		}



		[HttpGet]
		public async Task<IActionResult> HardDelete(int id)
		{

            var hardDeleteResult = await _categoryService.HardDeleteCategoryAsync(id);

            if (hardDeleteResult.IsSuccess)
            {
                NotifySuccess(hardDeleteResult.Message);


                return RedirectToAction(nameof(DeletedCategoryList), new { showWarning = false });
            }
            else
            {
                if (hardDeleteResult.Data == null)
                {
                    NotifyError(hardDeleteResult.Message);

                    return RedirectToAction(nameof(DeletedCategoryList), new { showWarning = false });
                }
                NotifyError(hardDeleteResult.Message);

                return RedirectToAction(nameof(DeletedCategoryList), new { showWarning = false });
            }
        }


		[HttpGet]
		public async Task<GetCategoryDto> GetCategory(int categoryid)
		{

			var categoryResult = await _categoryService.GetCategoryById(categoryid);

			return categoryResult.Data;
		}

		[HttpGet]
		public async Task<IActionResult> DeletedCategoryList(bool showWarning = true)
		{

			var deletedCategory = await _categoryService.GetAllByExpression(x => x.Status == Status.Deleted);
			List<DeletedCategoryListDto> deletedCategoryList = _mapper.Map<List<DeletedCategoryListDto>>(deletedCategory.Data);


			if ((deletedCategoryList.Count <= 0 || deletedCategoryList == null) && showWarning)
			{
				NotifyError("Silinen Kategori Listesi Boş");
			}
			else if (showWarning)
			{
				NotifySuccess("Silinen Kategoriler Listelendi");

			}
			return View(deletedCategoryList);

		}


		[HttpGet]
		public async Task<IActionResult> AddAgainCategory(int id)
		{
			var categoryToAdded = await _categoryService.GetCategoryById(id);

			if (categoryToAdded.Data == null)
			{
				NotifyError(categoryToAdded.Message);
				return RedirectToAction(nameof(DeletedCategoryList), new { showWarning = false });
			}
			else
			{

				categoryToAdded.Data.Status = Status.Active;
				UpdateCategoryDto updatedToCategory = _mapper.Map<UpdateCategoryDto>(categoryToAdded.Data);

				var categoryToUpdated = await _categoryService.UpdateCategoryAsync(updatedToCategory);
				NotifySuccess("Kategori yeniden eklendi.");

				return RedirectToAction(nameof(DeletedCategoryList), new { showWarning = false });
			}
		}

        [HttpGet]
        public async Task<DetailCategoryDto> GetDetailCategory(int categoryid)
        {

            var category = await _categoryService.GetCategoryById(categoryid);
            var categoryResult = _mapper.Map<DetailCategoryDto>(category.Data);

            return categoryResult;
        }




	}
}
