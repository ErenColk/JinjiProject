using JinjiProject.Core.Entities.Concrete;
using JinjiProject.Core.Utilities.Results.Concrete;
using JinjiProject.Dtos.Categories;
using JinjiProject.Dtos.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace JinjiProject.BusinessLayer.Managers.Abstract
{
    public interface IProductService
    {
        public Task<DataResult<Product>> CreateProductAsync(CreateProductDto createProductDto);
        public Task<DataResult<Product>> UpdateProductAsync(UpdateProductDto updateProductDto);
        public Task<DataResult<Product>> SoftDeleteProductAsync(int id);
        public Task<DataResult<Product>> HardDeleteProductAsync(int id);
        public Task<DataResult<List<ListProductDto>>> GetAllProduct();
        public Task<DataResult<GetProductDto>> GetProductById(int id);
        public Task<DataResult<GetProductDto>> GetFilteredProduct(Expression<Func<Product, bool>> expression);
        public Task<DataResult<List<ListProductDto>>> GetFilteredProductsAsync(Expression<Func<Product, bool>> expression);
    }
}
