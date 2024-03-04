using AutoMapper;
using JinjiProject.BusinessLayer.Managers.Abstract;
using JinjiProject.BusinessLayer.Validator.BrandValidations;
using JinjiProject.BusinessLayer.Validator.CategoryValidations;
using JinjiProject.Core.Enums;
using JinjiProject.Dtos.Brands;
using JinjiProject.Dtos.Categories;
using JinjiProject.UI.Controllers;
using Microsoft.AspNetCore.Mvc;

namespace JinjiProject.UI.Areas.Admin.Controllers
{
	[Area("Admin")]
	public class BrandController : BaseController
	{
		private readonly IBrandService _brandService;
		private readonly IMapper _mapper;

		public BrandController(IBrandService brandService,IMapper mapper)
        {
			_brandService = brandService;
			_mapper = mapper;
		}

		[HttpGet]
		public async Task<IActionResult> BrandList(bool showWarning = true)
		{
			var brandListResult = await _brandService.GetAllByExpression(brand=>brand.Status == Status.Active || brand.Status == Status.Modified);
			var brandList = _mapper.Map<List<ListBrandDto>>(brandListResult.Data);

            if ((brandList.Count <= 0 || brandList == null) && showWarning)
            {
                NotifyError(brandListResult.Message);
            }
            else if (showWarning)
            {
                NotifySuccess(brandListResult.Message);
            }

            return View(brandList);
		}

        [HttpGet]
        public async Task<IActionResult> CreateBrand()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateBrand(CreateBrandDto createBrandDto)
        {
            CreateBrandValidator validations = new CreateBrandValidator();
            var result = validations.Validate(createBrandDto);

            if (result.IsValid)
            {
                var createBrandResult = await _brandService.CreateBrandAsync(createBrandDto); ;
                if (createBrandResult.IsSuccess)
                {
                    NotifySuccess(createBrandResult.Message);
                }
                else
                {
                    NotifyError(createBrandResult.Message);

                }
                return RedirectToAction(nameof(BrandList), new { showWarning = false });
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

            return View(createBrandDto);
        }

        [HttpGet]
        public async Task<IActionResult> UpdateBrand(int id)
        { 
            var updateBrandResult = await _brandService.GetBrandById(id);
            if (updateBrandResult.IsSuccess)
            {
                UpdateBrandDto updateBrand = _mapper.Map<UpdateBrandDto>(updateBrandResult.Data);
                return View(updateBrand);

            }
            else
            {
                UpdateBrandDto updateBrand = _mapper.Map<UpdateBrandDto>(updateBrandResult.Data);
                NotifyError(updateBrandResult.Message);
                return RedirectToAction(nameof(BrandList), new { showWarning = false });
            }
            
        }


        [HttpPost]
        public async Task<IActionResult> UpdateBrand(UpdateBrandDto updateBrandDto)
        {
            UpdateBrandValidator updateBrandValidator = new UpdateBrandValidator();
            var result = updateBrandValidator.Validate(updateBrandDto);

            if (result.IsValid)
            {
                var updateBrandResult = await _brandService.UpdateBrandAsync(updateBrandDto);

                if (updateBrandResult.IsSuccess)
                {

                    NotifySuccess(updateBrandResult.Message);

                }
                else
                {
                    NotifyError(updateBrandResult.Message);
                }

                return RedirectToAction(nameof(BrandList), new { showWarning = false });
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

            return View(updateBrandDto);
        }

		[HttpGet]
		public async Task<IActionResult> SoftDelete(int id)
		{

			var softDeleteResult = await _brandService.SoftDeleteBrandAsync(id);

            if (softDeleteResult.IsSuccess)
            {
                NotifySuccess(softDeleteResult.Message);


                return RedirectToAction(nameof(BrandList), new { showWarning = false });
            }
            else
            {
                if (softDeleteResult.Data == null)
                {
                    NotifyError(softDeleteResult.Message);

                    return RedirectToAction(nameof(BrandList), new { showWarning = false });
                }
                NotifyError(softDeleteResult.Message);

                return RedirectToAction(nameof(BrandList), new { showWarning = false });
            }

		}


		[HttpGet]
		public async Task<IActionResult> HardDelete(int id)
		{

			var hardDeleteResult = await _brandService.HardDeleteBrandAsync(id);

            if (hardDeleteResult.IsSuccess)
            {
                NotifySuccess(hardDeleteResult.Message);


                return RedirectToAction(nameof(DeletedBrandList), new { showWarning = false });
            }
            else
            {
                if (hardDeleteResult.Data == null)
                {
                    NotifyError(hardDeleteResult.Message);

                    return RedirectToAction(nameof(DeletedBrandList), new { showWarning = false });
                }
                NotifyError(hardDeleteResult.Message);

                return RedirectToAction(nameof(DeletedBrandList), new { showWarning = false });
            }
        }

		[HttpGet]
		public async Task<IActionResult> DeletedBrandList(bool showWarning = true)
		{

			var deletedBrand = await _brandService.GetAllByExpression(x => x.Status == Status.Deleted);
			List<DeletedBrandListDto> deletedBrandList = _mapper.Map<List<DeletedBrandListDto>>(deletedBrand.Data);

            if ((deletedBrandList.Count <= 0 || deletedBrandList == null) && showWarning)
            {
                NotifyError("Silinen Marka Listesi Boş");
            }
            else if (showWarning)
            {
                NotifySuccess("Silinen Markalar Listelendi");

            }

            return View(deletedBrandList);

		}


		[HttpGet]
		public async Task<IActionResult> AddAgainBrand(int id)
		{
			var brandToAdded = await _brandService.GetBrandById(id);

			if (brandToAdded.Data == null)
			{
                NotifyError(brandToAdded.Message);
                return RedirectToAction(nameof(DeletedBrandList), new { showWarning = false });
            }
			else
			{

				brandToAdded.Data.Status = Status.Active;
				UpdateBrandDto updatedToBrand = _mapper.Map<UpdateBrandDto>(brandToAdded.Data);

				var brandToUpdated = await _brandService.UpdateBrandAsync(updatedToBrand);
                NotifySuccess("Marka yeniden eklendi.");

                return RedirectToAction(nameof(DeletedBrandList), new { showWarning = false });
            }
		}

        [HttpGet]
        public async Task<DetailBrandDto> GetDetailBrand(int brandid)
        {

            var brand = await _brandService.GetBrandById(brandid);
			var brandResult = _mapper.Map<DetailBrandDto>(brand.Data);

            return brandResult;
        }

        [HttpGet]
        public async Task<GetBrandDto> GetBrand(int brandid)
        {

            var brandResult = await _brandService.GetBrandById(brandid);

            return brandResult.Data;
        }

    }
}
