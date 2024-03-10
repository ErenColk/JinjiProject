using AutoMapper;
using JinjiProject.Core.Entities.Concrete;
using JinjiProject.Dtos.Brands;
using JinjiProject.Dtos.Categories;
using JinjiProject.VMs.Categories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JinjiProject.BusinessLayer.Profiles
{
    public class CategoryProfile : Profile
    {
        public CategoryProfile()
        {
            CreateMap<CreateCategoryDto, Category>().ReverseMap();
            CreateMap<ListCategoryHomePageVM, Category>().ReverseMap();
            CreateMap< UpdateCategoryDto, Category>()
    .ForMember(dest => dest.CreatedDate, opt => opt.Ignore())
    .ReverseMap(); ;
            CreateMap<ListCategoryDto, Category>().ReverseMap();
            CreateMap<GetCategoryDto, Category>().ReverseMap();
            CreateMap<GetCategoryDto, UpdateCategoryDto>().ReverseMap();
            CreateMap<ListCategoryDto, DeletedCategoryListDto>().ReverseMap();
            CreateMap<ListCategoryDto, ListHomePageCategory>().ReverseMap();
            CreateMap<Category, ListHomePageCategory>().ReverseMap();
            CreateMap<UpdateCategoryDto, ListHomePageCategory>().ReverseMap();
            CreateMap<UpdateCategoryDto, GetCategoryDto>().ReverseMap();
            CreateMap<DetailCategoryDto, GetCategoryDto>().ReverseMap();

        }


    }
}
