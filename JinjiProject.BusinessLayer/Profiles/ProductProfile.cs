using AutoMapper;
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
            CreateMap<CreateProductDto, Product>()
           .ForMember(dest => dest.Size, opt => opt.MapFrom(src => GetSizeByValue(Convert.ToInt32(src.SizeId)))).ReverseMap();
            CreateMap<Product, UpdateProductDto>().ReverseMap()
                .ForMember(dest => dest.Size, opt => opt.MapFrom(src => GetSizeByValue(src.SizeId)));
            CreateMap<UpdateProductDto, GetProductDto>().ReverseMap()               ;
            CreateMap<ListProductDto, Product>().ReverseMap();
            CreateMap<ListProductDto, DeletedProductListDto>().ReverseMap();
            CreateMap<GetProductDto, Product>().ReverseMap();
            CreateMap<GetProductDto, CreateProductDto>().ReverseMap();
        }

        private Size GetSizeByValue(int sizeId)
        {
            switch (sizeId)
            {
                case 0:
                    return Size.Small;
                case 1:
                    return Size.Medium;
                case 2:
                    return Size.Large;
                default:
                    throw new ArgumentException("Geçersiz sizeId: " + sizeId);
            }
        }
    }
}
