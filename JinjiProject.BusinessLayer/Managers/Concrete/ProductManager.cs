using AutoMapper;
using JinjiProject.BusinessLayer.Constants;
using JinjiProject.BusinessLayer.Managers.Abstract;
using JinjiProject.Core.Entities.Concrete;
using JinjiProject.Core.Enums;
using JinjiProject.Core.Utilities.Results.Concrete;
using JinjiProject.DataAccess.EFCore.Repositories;
using JinjiProject.DataAccess.Interface.Repositories;
using JinjiProject.Dtos.Admins;
using JinjiProject.Dtos.Categories;
using JinjiProject.Dtos.Products;
using Microsoft.EntityFrameworkCore;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace JinjiProject.BusinessLayer.Managers.Concrete
{


    public class ProductManager : IProductService
    {
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;

        public ProductManager(IProductRepository ProductRepository, IMapper mapper)
        {
            _productRepository = ProductRepository;
            _mapper = mapper;
        }

        public async Task<DataResult<Product>> CreateProductAsync(CreateProductDto createProductDto)
        {
            if (createProductDto == null)
            {
                return new ErrorDataResult<Product>(Messages.CreateProductError);
            }
            else
            {
                if (createProductDto.UploadPath != null)
                {
                    using (var image = Image.Load(createProductDto.UploadPath.OpenReadStream()))
                    {
                        Guid guid = Guid.NewGuid();
                        image.Save($"wwwroot/images/productPhotos/{guid}{Path.GetExtension(createProductDto.UploadPath.FileName)}");
                        createProductDto.ImagePath = $"/images/productPhotos/{guid}{Path.GetExtension(createProductDto.UploadPath.FileName)}";
                    }                   
                }

                Product Product = _mapper.Map<Product>(createProductDto);

                bool result = await _productRepository.Create(Product);
                if (result)
                    return new SuccessDataResult<Product>(Product, Messages.CreateProductSuccess);
                else
                    return new ErrorDataResult<Product>(Product, Messages.CreateProductRepoError);

            }
        }

        public async Task<DataResult<GetProductDto>> GetProductById(int id)
        {
            if (id <= 0)
                return new ErrorDataResult<GetProductDto>(Messages.ProductNotFound);
            else
            {
                GetProductDto getProductDto = _mapper.Map<GetProductDto>(await _productRepository.GetByIdAsync(id));
                return new SuccessDataResult<GetProductDto>(getProductDto, Messages.ProductFoundSuccess);
            }


        }

        public async Task<DataResult<List<ListProductDto>>> GetAllProduct()
        {
            var products = await _productRepository.GetAllAsync(false);
            return new SuccessDataResult<List<ListProductDto>>(_mapper.Map<List<ListProductDto>>(products), Messages.ProductListedSuccess);
        }

        public async Task<DataResult<GetProductDto>> GetFilteredProduct(Expression<Func<Product, bool>> expression)
        {
            var ProductDto = await _productRepository.GetFilteredFirstOrDefault(expression);
            if (ProductDto == null)
            {
                return new ErrorDataResult<GetProductDto>(Messages.ProductFilteredError);
            }
            else
            {
                GetProductDto getProductDto = _mapper.Map<GetProductDto>(ProductDto);
                return new SuccessDataResult<GetProductDto>(getProductDto, Messages.ProductFilteredSuccess);
            }
        }

        public async Task<DataResult<Product>> HardDeleteProductAsync(int id)
        {
            var ProductDto = await _productRepository.GetByIdAsync(id);
            if (ProductDto == null)
            {
                return new ErrorDataResult<Product>(Messages.ProductNotFound);
            }
            else
            {
                bool result = await _productRepository.HardDelete(ProductDto);
                if (result)
                {
                    if (File.Exists($"wwwroot/{ProductDto.ImagePath}"))
                    {
                        File.Delete($"wwwroot/{ProductDto.ImagePath}");
                    }
                    return new SuccessDataResult<Product>(Messages.ProductDeletedSuccess);
                }
                else
                    return new ErrorDataResult<Product>(Messages.ProductDeletedRepoError);
            }
        }

        public async Task<DataResult<Product>> SoftDeleteProductAsync(int id)
        {
            var ProductDto = await _productRepository.GetByIdAsync(id);
            if (ProductDto == null)
            {
                return new ErrorDataResult<Product>(Messages.ProductNotFound);
            }
            else
            {
                bool result = await _productRepository.SoftDelete(ProductDto);
                if (result)
                    return new SuccessDataResult<Product>(Messages.ProductDeletedSuccess);
                else
                    return new ErrorDataResult<Product>(Messages.ProductDeletedRepoError);
            }
        }

        public async Task<DataResult<Product>> UpdateProductAsync(UpdateProductDto updateProductDto)
        {
            if (updateProductDto == null)
            {
                return new ErrorDataResult<Product>(Messages.UpdateProductError);
            }
            else
            {
                Product product = await _productRepository.GetByIdAsync(updateProductDto.Id);

                if (updateProductDto.UploadPath != null)
                {
                    using (var image = Image.Load(updateProductDto.UploadPath.OpenReadStream()))
                    {
                        Guid guid = Guid.NewGuid();
                        image.Save($"wwwroot/images/productPhotos/{guid}{Path.GetExtension(updateProductDto.UploadPath.FileName)}");
                        updateProductDto.ImagePath = $"/images/productPhotos/{guid}{Path.GetExtension(updateProductDto.UploadPath.FileName)}";
                    }                   
                }

                product = _mapper.Map(updateProductDto, product);
                bool result = await _productRepository.Update(product);
                if (result)
                    return new SuccessDataResult<Product>(product, Messages.UpdateProductSuccess);
                else
                    return new ErrorDataResult<Product>(product, Messages.UpdateProductRepoError);
            }
        }

        public async Task<DataResult<List<ListProductDto>>> GetFilteredProductsAsync(Expression<Func<Product, bool>> expression)
        {
            var products = await _productRepository.GetAllIncludeByExpression(expression);
            if (products == null)
            {
                return new ErrorDataResult<List<ListProductDto>>(Messages.ProductFilteredError);
            }
            else
            {
                List<ListProductDto> listProductDto = _mapper.Map<List<ListProductDto>>(products);
                return new SuccessDataResult<List<ListProductDto>>(listProductDto, Messages.ProductFilteredSuccess);
            }
        }

        public async Task<DataResult<List<ListProductDto>>> GetAllByExpression(Expression<Func<Product, bool>> expression)
        {
            var products = await _productRepository.GetAllByExpression(expression);

            if (products.Count() <= 0 || products == null)
            {
                return new ErrorDataResult<List<ListProductDto>>(Messages.ProductListedEmpty);
            }
            else
            {
                return new SuccessDataResult<List<ListProductDto>>(_mapper.Map<List<ListProductDto>>(products), Messages.ProductListedSuccess);

            }

        }

        public async Task<DataResult<List<ListProductDto>>> GetProductBySearchValues(string? name, string? price, string? materialId, string? genreId, string? createdDate)
        {
            int nullParamCount = new[] { name, price, materialId, genreId }.Count(param => param != null);
            
            if (DateTime.Parse(createdDate).Year.ToString() != "1")
            {
                nullParamCount++;
            }

            var productsByName = await _productRepository.GetAllByExpression(product => product.Status != Status.Deleted && product.Name.Contains(name));
            var productsByPrice = await _productRepository.GetAllByExpression(product => product.Status != Status.Deleted && product.Price <= Convert.ToDecimal(price));
            var productsByMaterial = await _productRepository.GetAllByExpression(product => product.Status != Status.Deleted && product.MaterialId == Convert.ToInt32(materialId));
            var productsByGenre = await _productRepository.GetAllByExpression(product => product.Status != Status.Deleted && product.GenreId == Convert.ToInt32(genreId));
            var productsByCreatedYear = await _productRepository.GetAllByExpression(product => product.Status != Status.Deleted && EF.Functions.DateDiffDay(product.CreatedDate, DateTime.Parse(createdDate)) == 0);

            var filteredProducts = IntersectNonEmpty(nullParamCount, productsByName, productsByPrice, productsByMaterial, productsByGenre, productsByCreatedYear);

            return filteredProducts.Any()
            ? new SuccessDataResult<List<ListProductDto>>(_mapper.Map<List<ListProductDto>>(filteredProducts), Messages.ProductListedSuccess)
            : new ErrorDataResult<List<ListProductDto>>(Messages.ProductNotFound);
        }

        private static IEnumerable<T> IntersectNonEmpty<T>(int _nullParamCount, params IEnumerable<T>[] lists)
        {
            var nonEmptyLists = lists.Where(list => list != null && list.Any()).ToList();

            if (nonEmptyLists.Count == 0)
            {
                return new List<T>();
            }

            IEnumerable<T> result = nonEmptyLists[0];
            if (_nullParamCount == nonEmptyLists.Count)
            {
                for (int i = 1; i < nonEmptyLists.Count; i++)
                {
                    result = result.Intersect(nonEmptyLists[i]).ToList();

                    if (!result.Any())
                    {
                        break;
                    }
                }
                return result;
            }
            else
            {

                return result = new List<T>();
            }
        }

        public async Task<DataResult<Product>> UpdateProductDiscountAsync(UpdateProductDiscountDto updateProductDiscountDto)
        {
            if (updateProductDiscountDto == null)
            {
                return new ErrorDataResult<Product>(Messages.UpdateProductError);
            }
            else
            {
                Product product = await _productRepository.GetByIdAsync(updateProductDiscountDto.Id);

                            product = _mapper.Map(updateProductDiscountDto, product);
                bool result = await _productRepository.Update(product);
                if (result)
                    return new SuccessDataResult<Product>(product, Messages.UpdateProductSuccess);
                else
                    return new ErrorDataResult<Product>(product, Messages.UpdateProductRepoError);
            }
        }
    }
}
