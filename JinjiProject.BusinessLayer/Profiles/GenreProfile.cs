using AutoMapper;
using JinjiProject.BusinessLayer.Helpers;
using JinjiProject.Core.Entities.Concrete;
using JinjiProject.Dtos.Brands;
using JinjiProject.Dtos.Categories;
using JinjiProject.Dtos.Genres;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JinjiProject.BusinessLayer.Profiles
{
    public class GenreProfile : Profile
    {
        public GenreProfile()
        {
            CreateMap<CreateGenreDto, Genre>().ReverseMap();
            CreateMap<Genre, UpdateGenreDto>().ReverseMap()
                .ForMember(dest => dest.ImagePath, opt => opt.Condition(src => src.ImagePath != null));
            CreateMap<ListGenreDto, Genre>().ReverseMap()
                .ForMember(dest => dest.CategoryName, opt => opt.MapFrom(src => src.Category.Name)).ForMember(dest => dest.StatusName, opt => opt.MapFrom(opt => GetEnumDescription.Description(opt.Status)));
            CreateMap<GetGenreDto, Genre>().ReverseMap();
            CreateMap<GetGenreDto, UpdateGenreDto>().ReverseMap();
            CreateMap<ListGenreDto, DeletedGenreListDto>().ReverseMap();
            CreateMap<ListGenreDto, ListHomePageGenreDto>().ReverseMap();
            CreateMap<UpdateGenreDto, ListHomePageGenreDto>().ReverseMap();
            CreateMap<UpdateHomePageGenreDto, ListHomePageGenreDto>().ReverseMap();
            CreateMap<UpdateHomePageGenreDto, Genre>().ReverseMap();
            CreateMap<UpdateGenreDto, GetGenreDto>().ReverseMap();
            CreateMap<DetailGenreDto, GetGenreDto>().ReverseMap();
            CreateMap<Genre, ListHomePageGenreDto>().ReverseMap();
        }
    }
}
