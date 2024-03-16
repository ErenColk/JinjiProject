using AutoMapper;
using JinjiProject.BusinessLayer.Helpers;
using JinjiProject.Core.Entities.Concrete;
using JinjiProject.Core.Enums;
using JinjiProject.Dtos.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JinjiProject.BusinessLayer.Profiles
{
    public class ProductProfile : Profile
    {
        public ProductProfile()
        {
            CreateMap<CreateProductDto, Product>().ReverseMap();
            CreateMap<Product, UpdateProductDto>().ReverseMap();
            CreateMap<GetProductDto, UpdateProductDto>().ReverseMap();
            CreateMap<Product, ListProductDto>().ForMember(dest => dest.GenreName, opt => opt.MapFrom(src => src.Genre.Name)).ReverseMap();
            CreateMap<ListProductDto, DeletedProductListDto>().ReverseMap();
            CreateMap<GetProductDto, Product>().ReverseMap();
            CreateMap<GetProductDto, CreateProductDto>().ReverseMap();
        }
    
    }
}
