using AutoMapper;
using JinjiProject.Core.Entities.Concrete;
using JinjiProject.Dtos.Brands;
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
            CreateMap<UpdateGenreDto, Genre>().ReverseMap();
            CreateMap<ListGenreDto, Genre>().ReverseMap();
            CreateMap<GetGenreDto, Genre>().ReverseMap();
            CreateMap<GetGenreDto, UpdateGenreDto>().ReverseMap();
            CreateMap<ListGenreDto, DeletedGenreListDto>().ReverseMap();
            CreateMap<UpdateGenreDto, GetGenreDto>().ReverseMap();
            CreateMap<DetailGenreDto, GetGenreDto>().ReverseMap();
        }
    }
}
