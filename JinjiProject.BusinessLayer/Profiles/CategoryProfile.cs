using AutoMapper;
using JinjiProject.Core.Entities.Concrete;
using JinjiProject.Dtos.Categories;
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
            CreateMap<UpdateCategoryDto, Category>().ReverseMap();
            CreateMap<ListCategoryDto, Category>().ReverseMap();
            CreateMap<GetCategoryDto, Category>().ReverseMap();


        }


    }
}
