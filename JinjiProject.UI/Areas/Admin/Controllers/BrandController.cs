using AutoMapper;
using JinjiProject.BusinessLayer.Managers.Abstract;
using JinjiProject.Core.Enums;
using JinjiProject.Dtos.Brands;
using JinjiProject.Dtos.Categories;
using Microsoft.AspNetCore.Mvc;

namespace JinjiProject.UI.Areas.Admin.Controllers
{
	[Area("Admin")]
	public class BrandController : Controller
	{
		private readonly IBrandService _brandService;
		private readonly IMapper _mapper;

		public BrandController(IBrandService brandService,IMapper mapper)
        {
			_brandService = brandService;
			_mapper = mapper;
		}

		[HttpGet]
		public async Task<IActionResult> BrandList()
		{
			var brandListResult = await _brandService.GetAllByExpression(brand=>brand.Status == Status.Active || brand.Status == Status.Modified);
			var brandList = _mapper.Map<List<ListBrandDto>>(brandListResult.Data);
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
            var createResult = await _brandService.CreateBrandAsync(createBrandDto); ;

            return RedirectToAction(nameof(BrandList));
        }

        [HttpGet]
        public async Task<IActionResult> UpdateBrand(int id)
        { 
            var updateBrandResult = await _brandService.GetBrandById(id);
            UpdateBrandDto updateBrand = _mapper.Map<UpdateBrandDto>(updateBrandResult.Data);
            return View(updateBrand);
        }


        [HttpPost]
        public async Task<IActionResult> UpdateBrand(UpdateBrandDto updateBrandDto)
        {
            var updateBrand = await _brandService.UpdateBrandAsync(updateBrandDto);

            return RedirectToAction(nameof(BrandList));
        }

		[HttpGet]
		public async Task<IActionResult> SoftDelete(int id)
		{

			await _brandService.SoftDeleteBrandAsync(id);

			return RedirectToAction(nameof(BrandList));
		}


		[HttpGet]
		public async Task<IActionResult> HardDelete(int id)
		{

			await _brandService.SoftDeleteBrandAsync(id);

			return RedirectToAction(nameof(BrandList));
		}

		[HttpGet]
		public async Task<IActionResult> DeletedBrandList()
		{

			var deletedBrand = await _brandService.GetAllByExpression(x => x.Status == Status.Deleted);
			List<DeletedBrandListDto> deletedBrandList = _mapper.Map<List<DeletedBrandListDto>>(deletedBrand.Data);
			return View(deletedBrandList);

		}


		[HttpGet]
		public async Task<IActionResult> AddAgainBrand(int id)
		{
			var brandToAdded = await _brandService.GetBrandById(id);

			if (brandToAdded.Data == null)
			{
				return View();
			}
			else
			{

				brandToAdded.Data.Status = Status.Active;
				UpdateBrandDto updatedToBrand = _mapper.Map<UpdateBrandDto>(brandToAdded.Data);
				await _brandService.UpdateBrandAsync(updatedToBrand);
				return RedirectToAction("BrandList", "Brand");
			}
		}

        [HttpGet]
        public async Task<IActionResult> BrandDetails(int id)
        {
            var brand = await _brandService.GetBrandById(id);

			if(brand.Data == null)
			{
				return View();
			}
			else
			{
				DetailBrandDto detailBrandDto = _mapper.Map<DetailBrandDto>(brand.Data);
				return View(detailBrandDto);
			}
        }

    }
}
