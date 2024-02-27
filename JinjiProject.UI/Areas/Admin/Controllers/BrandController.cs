using AutoMapper;
using JinjiProject.BusinessLayer.Managers.Abstract;
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
			var brandListResult = await _brandService.GetAllBrand();
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

    }
}
