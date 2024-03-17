using AutoMapper;
using JinjiProject.BusinessLayer.Helpers;
using JinjiProject.Core.Entities.Concrete;
using JinjiProject.Dtos.Brands;
using JinjiProject.Dtos.Categories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JinjiProject.BusinessLayer.Profiles
{
    public class BrandProfile : Profile
    {
        public BrandProfile()
        {
            CreateMap<CreateBrandDto, Brand>().ReverseMap();
            CreateMap<UpdateBrandDto, Brand>().ReverseMap();
            CreateMap<ListBrandDto, Brand>().ReverseMap()
                .ForMember(dest => dest.StatusName, opt => opt.MapFrom(opt => GetEnumDescription.Description(opt.Status)));
            CreateMap<GetBrandDto, Brand>().ReverseMap();
            CreateMap<GetBrandDto, UpdateBrandDto>().ReverseMap();
			CreateMap<ListBrandDto, DeletedBrandListDto>().ReverseMap();
			CreateMap<UpdateBrandDto, GetBrandDto>().ReverseMap();
			CreateMap<DetailBrandDto, GetBrandDto>().ReverseMap();
		
		}
    }
}
