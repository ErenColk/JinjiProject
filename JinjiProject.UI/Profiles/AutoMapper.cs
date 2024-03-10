using AutoMapper;
using JinjiProject.Dtos.Categories;
using JinjiProject.UI.Models.CategoryVMs;

namespace JinjiProject.UI.Profiles
{
    public class AutoMapper : Profile
    {
        public AutoMapper()
        {
            // Category Mapper
            CreateMap<ListCategoryDto, CategoryNameVM>().ReverseMap();
        }
    }
}
