using AutoMapper;
using JinjiProject.BusinessLayer.Helpers;
using JinjiProject.Core.Entities.Concrete;
using JinjiProject.Dtos.Brands;
using JinjiProject.Dtos.Categories;
using JinjiProject.Dtos.Materials;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JinjiProject.BusinessLayer.Profiles
{
    public class MaterialProfile : Profile
    {
        public MaterialProfile()
        {
            CreateMap<CreateMaterialDto, Material>().ReverseMap();
            CreateMap<UpdateMaterialDto, Material>().ReverseMap();
            CreateMap<ListMaterialDto, Material>().ReverseMap()
                .ForMember(dest => dest.StatusName, opt => opt.MapFrom(opt => GetEnumDescription.Description(opt.Status)));
            CreateMap<GetMaterialDto, Material>().ReverseMap();
            CreateMap<GetMaterialDto, UpdateMaterialDto>().ReverseMap();
            CreateMap<ListMaterialDto, DeletedMaterialListDto>().ReverseMap();
            CreateMap<UpdateMaterialDto, GetMaterialDto>().ReverseMap();
            CreateMap<DetailMaterialDto, GetMaterialDto>().ReverseMap();
        }
    }
}
