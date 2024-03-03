using AutoMapper;
using JinjiProject.Core.Entities.Concrete;
using JinjiProject.Dtos.Admins;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JinjiProject.BusinessLayer.Profiles
{
    public class AdminProfile : Profile
    {
        public AdminProfile()
        {
            CreateMap<CreateAdminDto, Admin>().ReverseMap();
            CreateMap<UpdateAdminDto, Admin>().ReverseMap()
                .ForMember(dest => dest.FirstName, opt => opt.Condition(src => src.FirstName != null))
                .ForMember(dest => dest.LastName, opt => opt.Condition(src => src.LastName != null))
                .ForMember(dest => dest.BirthDate, opt => opt.Condition(src => src.BirthDate != null))
                .ForMember(dest => dest.Gender, opt => opt.Condition(src => src.Gender != null))
                .ForMember(dest => dest.ImagePath, opt => opt.Condition(src => src.ImagePath != null));
            CreateMap<UpdateAdminDto, GetAdminDto>().ReverseMap();
            CreateMap<GetAdminDto, Admin>().ReverseMap();
            CreateMap<ListAdminDto, Admin>().ReverseMap();
            CreateMap<DeletedAdminListDto, ListAdminDto>().ReverseMap();

         

        }
    }
}
