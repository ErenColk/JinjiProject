using JinjiProject.BusinessLayer.Managers.Abstract;
using JinjiProject.Core.Entities.Concrete;
using JinjiProject.Dtos.Products;
using Microsoft.AspNetCore.Mvc;

namespace JinjiProject.UI.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductService productService;

        public ProductController(IProductService productService)
        {
            this.productService = productService;
        }
        [HttpGet]
        public async Task<IActionResult> Index(string category = null)
        {
            var products = await productService.GetFilteredProductsAsync(product => product.Category.Name == category && product.Status != Core.Enums.Status.Deleted);
            if (products.IsSuccess)
            {
                if (products.Data.Count > 0)
                {
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


        [HttpGet]
        public async Task<IActionResult> BagList()
        {
            var productList = await productService.GetAllProduct();  

            return PartialView("_BagListPartialView", productList.Data);

        }




    }
}
