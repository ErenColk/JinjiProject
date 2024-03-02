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
            CreateMap<UpdateAdminDto, Admin>().ReverseMap();
            CreateMap<GetAdminDto, Admin>().ReverseMap();
            CreateMap<ListAdminDto, Admin>().ReverseMap();
            CreateMap<DeletedAdminListDto, ListAdminDto>().ReverseMap();

         

        }
    }
}
