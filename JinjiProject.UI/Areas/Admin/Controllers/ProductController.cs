using AutoMapper;
using JinjiProject.BusinessLayer.Helpers.SelectItemProducts;
using JinjiProject.BusinessLayer.Managers.Abstract;
using JinjiProject.BusinessLayer.Validator.ProductValidations;
using JinjiProject.Core.Entities.Concrete;
using JinjiProject.Core.Enums;
using JinjiProject.Dtos.Brands;
using JinjiProject.Dtos.Categories;
using JinjiProject.Dtos.Materials;
using JinjiProject.Dtos.Products;
using JinjiProject.UI.Controllers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace JinjiProject.UI.Areas.Admin.Controllers
{
    public class ProductController : AdminBaseController
    {

        private readonly IProductService _productService;
        private readonly IBrandService _brandService;
        private readonly ICategoryService _categoryService;
        private readonly IMaterialService _materialService;
        private readonly IMapper _mapper;
        private readonly IGenreService _genreService;

        public ProductController(IProductService productService, IBrandService brandService, ICategoryService categoryService, IMaterialService materialService, IMapper mapper, IGenreService genreService)
        {
            _productService = productService;
            _brandService = brandService;
            _categoryService = categoryService;
            _materialService = materialService;
            _mapper = mapper;
            _genreService = genreService;
        }

        [HttpGet]
        public async Task<IActionResult> ProductList(bool showWarning = true)
        {
            var productListResult = await _productService.GetAllByExpression(product => product.Status != Status.Deleted);
            var productList = _mapper.Map<List<ListProductDto>>(productListResult.Data);

            if ((productList.Count <= 0 || productList == null) && showWarning)
            {
                NotifyError(productListResult.Message);
            }
            else if (showWarning)
            {
                NotifySuccess(productListResult.Message);
            }

            return View(productList);
        }


        [HttpGet]
        public async Task<IActionResult> CreateProduct()
        {

            var listBrandDto = await _brandService.GetAllByExpression(brand => brand.Status != Status.Deleted);
            var listCategoryDto = await _categoryService.GetAllByExpression(category => category.Status != Status.Deleted);
            var listMaterialDto = await _materialService.GetAllByExpression(material => material.Status != Status.Deleted);


            ViewBag.Brands = await BrandItems.GetBrands(listBrandDto.Data);
            ViewBag.Categories = await CategoryItems.GetCategory(listCategoryDto.Data);
            ViewBag.Materials = await MaterialItems.GetMaterial(listMaterialDto.Data);
            ViewBag.Size = await SizeItems.GetSize();
            return View();
        }



        [HttpPost]
        public async Task<IActionResult> CreateProduct(CreateProductDto createProductDto)
        {

            CreateProductValidator validations = new CreateProductValidator();
            var result = validations.Validate(createProductDto);

            if (result.IsValid)
            {
                var createProductResult = await _productService.CreateProductAsync(createProductDto);
                if (createProductResult.IsSuccess)
                {
                    NotifySuccess(createProductResult.Message);
                }
                else
                {
                    NotifyError(createProductResult.Message);

                }
                return RedirectToAction(nameof(ProductList), new { showWarning = false });
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
            return View(createProductDto);

        }



        [HttpGet]
        public async Task<IActionResult> UpdateProduct(int id)
        {
            var updateProductResult = await _productService.GetProductById(id);
            if (updateProductResult.IsSuccess)
            {
                var selectedCategory = await _categoryService.GetCategoryFilteredInclude(category => category.Genres.Any(genre => genre.Id.ToString() == updateProductResult.Data.GenreId));
                updateProductResult.Data.CategoryId = selectedCategory.Data.Id.ToString();

                var listBrandDto = await _brandService.GetAllByExpression(brand => brand.Status != Status.Deleted);
                var listCategoryDto = await _categoryService.GetAllByExpression(category => category.Status != Status.Deleted);
                var listMaterialDto = await _materialService.GetAllByExpression(material => material.Status != Status.Deleted);
                var selectedGenre = await _genreService.GetAllByExpression(genre => genre.Status != Status.Deleted && genre.CategoryId == selectedCategory.Data.Id);

                ViewBag.Brands = await BrandItems.GetBrands(listBrandDto.Data);
                ViewBag.Categories = await CategoryItems.GetCategory(listCategoryDto.Data);
                ViewBag.Materials = await MaterialItems.GetMaterial(listMaterialDto.Data);
                ViewBag.Size = await SizeItems.GetSize();
                ViewBag.Genres = await GenreItems.GetGenres(selectedGenre.Data);
                UpdateProductDto updateProduct = _mapper.Map<UpdateProductDto>(updateProductResult.Data);
                return View(updateProduct);

            }
            else
            {
                NotifyError(updateProductResult.Message);
                return RedirectToAction(nameof(ProductList), new { showWarning = false });
            }

        }


        [HttpPost]
        public async Task<IActionResult> UpdateProduct(UpdateProductDto updateProductDto)
        {
            UpdateProductValidator updateProductValidator = new UpdateProductValidator();
            var result = updateProductValidator.Validate(updateProductDto);

            if (result.IsValid)
            {
                var updateProductResult = await _productService.UpdateProductAsync(updateProductDto);

                if (updateProductResult.IsSuccess)
                {

                    NotifySuccess(updateProductResult.Message);

                }
                else
                {
                    NotifyError(updateProductResult.Message);
                }

                return RedirectToAction(nameof(ProductList), new { showWarning = false });
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
            return View(updateProductDto);


        }


        [HttpGet]
        public async Task<IActionResult> SoftDelete(int id)
        {

            var softDeleteResult = await _productService.SoftDeleteProductAsync(id);


            if (softDeleteResult.IsSuccess)
            {
                NotifySuccess(softDeleteResult.Message);


                return RedirectToAction(nameof(ProductList), new { showWarning = false });
            }
            else
            {
                if (softDeleteResult.Data == null)
                {
                    NotifyError(softDeleteResult.Message);

                    return RedirectToAction(nameof(ProductList), new { showWarning = false });
                }
                NotifyError(softDeleteResult.Message);

                return RedirectToAction(nameof(ProductList), new { showWarning = false });
            }


        }



        [HttpGet]
        public async Task<IActionResult> HardDelete(int id)
        {

            await _productService.HardDeleteProductAsync(id);

            return RedirectToAction(nameof(ProductList));
        }


        [HttpGet]
        public async Task<GetProductDto> GetProduct(int productid)
        {

            var productResult = await _productService.GetProductById(productid);

            return productResult.Data;
        }

        [HttpGet]
        public async Task<IActionResult> DeletedProductList(bool showWarning = true)
        {

            var deletedProduct = await _productService.GetAllByExpression(x => x.Status == Status.Deleted);
            List<DeletedProductListDto> deletedProductList = _mapper.Map<List<DeletedProductListDto>>(deletedProduct.Data);


            if ((deletedProductList.Count <= 0 || deletedProductList == null) && showWarning)
            {
                NotifyError("Silinen Kategori Listesi Boş");
            }
            else if (showWarning)
            {
                NotifySuccess("Silinen Kategoriler Listelendi");

            }
            return View(deletedProductList);

        }


        [HttpGet]
        public async Task<IActionResult> AddAgainProduct(int id)
        {
            var productToAdded = await _productService.GetProductById(id);

            if (productToAdded.Data == null)
            {
                NotifyError(productToAdded.Message);
                return RedirectToAction(nameof(DeletedProductList), new { showWarning = false });
            }
            else
            {

                productToAdded.Data.Status = Status.Active;
                UpdateProductDto updatedToProduct = _mapper.Map<UpdateProductDto>(productToAdded.Data);

                var productToUpdated = await _productService.UpdateProductAsync(updatedToProduct);
                NotifySuccess("Ürün yeniden eklendi.");

                return RedirectToAction(nameof(DeletedProductList), new { showWarning = false });
            }
        }


        [HttpGet]
        public async Task<SelectList> AddGenreList(int id)
        {


            var deneme = await _genreService.GetAllByExpression(genre => genre.Status != Status.Deleted && genre.CategoryId == id);

            return new SelectList((await _genreService.GetAllByExpression(genre => genre.Status != Status.Deleted && genre.CategoryId == id)).Data, "Id", "Name");

        }


    }
}
