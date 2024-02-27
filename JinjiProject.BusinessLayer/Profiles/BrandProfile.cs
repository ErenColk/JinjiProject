using AutoMapper;
using JinjiProject.Core.Entities.Concrete;
using JinjiProject.Dtos.Brands;
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
            CreateMap<ListBrandDto, Brand>().ReverseMap();
            CreateMap<GetBrandDto, Brand>().ReverseMap();
            CreateMap<GetBrandDto, UpdateBrandDto>().ReverseMap();
        }
    }
}
