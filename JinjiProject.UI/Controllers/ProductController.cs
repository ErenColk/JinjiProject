﻿using JinjiProject.BusinessLayer.Managers.Abstract;
using JinjiProject.Core.Entities.Concrete;
using JinjiProject.Core.Utilities.Results.Concrete;
using JinjiProject.Dtos.Products;
using Microsoft.AspNetCore.Mvc;

namespace JinjiProject.UI.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductService productService;
        private readonly ICategoryService _categoryService;

        public ProductController(IProductService productService,ICategoryService categoryService)
        {
            this.productService = productService;
            _categoryService = categoryService;
        }
        [HttpGet]
        public async Task<IActionResult> Index(string category = null,string genre = null)
        {
            if(category == null)
            {
                var products = await productService.GetFilteredProductsAsync(product => product.Genre.Name == genre && product.Status != Core.Enums.Status.Deleted);
                if (products.IsSuccess)
                {
                    if (products.Data.Count > 0)
                    {
                    var categoryResult = await _categoryService.GetFilteredCategory(x => x.Genres.Any(src=> src.Id == products.Data.FirstOrDefault().GenreId));

                        ViewBag.CategoryName = categoryResult.Data.Name;
                        ViewBag.Genre = genre;
                        return View(products.Data);
                    }
                    else
                        return View();
                }
                else
                {
                    return View();
                }
            }
            else
            {
                var products = await productService.GetFilteredProductsAsync(product => product.Genre.Category.Name == category && product.Status != Core.Enums.Status.Deleted);
                if (products.IsSuccess)
                {
                    if (products.Data.Count > 0)
                    {
                        var categoryResult = await _categoryService.GetFilteredCategory(x => x.Genres.Any(src => src.Id == products.Data.FirstOrDefault().GenreId));

                        ViewBag.CategoryName = categoryResult.Data.Name;
                        ViewBag.Genre = genre;
                        return View(products.Data);
                    }
                    else
                        return View();
                }
                else
                {
                    return View();
                }
            }               
        }


        [HttpGet]
        public async Task<IActionResult> BagList()
        {
            var categoryList = await _categoryService.GetListCategoryIncludeOrderBy(category => category.IsOnHomePage == true);
            return PartialView("_BagListPartialView", categoryList.Data);

        }

        [HttpGet]
        public async Task<IActionResult> ProductDetails(int id)
        {
            var product = await productService.GetProductById(id);
            if (product.IsSuccess)
            {
                ViewBag.ProductName = product.Data.Name;
                return View(product.Data);
            }
            else
            {
                return BadRequest();
            }
        }


        [HttpGet]
        public async Task<object> GetSizeNames()
        {
            var sizeNames = Enum.GetNames(typeof(Core.Enums.Size));
            return sizeNames;
        }

        [HttpGet]
        public async Task<object> GetColorNames()
        {
            var products = await productService.GetAllByExpression(x => x.Status != Core.Enums.Status.Deleted);
            List<string> colorNames = new List<string>();
            foreach (var item in products.Data)
            {
                colorNames.Add(item.Color);
            }
            var distinctColorNames = colorNames.Distinct();
            return distinctColorNames;
        }

        [HttpGet]
        public async Task<object> GetGenreNames(string categoryName)
        {
            var products = await productService.GetAllByExpression(x => x.Status != Core.Enums.Status.Deleted && x.Genre.Category.Name == categoryName);
            List<string> genreNames = new List<string>();
            foreach (var item in products.Data)
            {
                genreNames.Add(item.GenreName);
            }
            var distinctGenreNames = genreNames.Distinct();
            return distinctGenreNames;
        }
    }
}
