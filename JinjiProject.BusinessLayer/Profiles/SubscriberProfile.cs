using AutoMapper;
using JinjiProject.Core.Entities.Concrete;
using JinjiProject.Dtos.Brands;
using JinjiProject.Dtos.Subscribers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JinjiProject.BusinessLayer.Profiles
{
    public class SubscriberProfile : Profile
    {
        public SubscriberProfile()
        {
            CreateMap<CreateSubscriberDto, Subscriber>().ReverseMap();
            CreateMap<ListSubscriberDto, Subscriber>().ReverseMap();
            CreateMap<GetSubscriberDto, Subscriber>().ReverseMap();
            CreateMap<GetSubscriberDto, DetailSubscriberDto>().ReverseMap();
            CreateMap<UpdateSubscriberDto, Subscriber>().ReverseMap();
            CreateMap<UpdateSubscriberDto, GetSubscriberDto>().ReverseMap();
            CreateMap<ListSubscriberDto, DeletedSubscriberListDto>().ReverseMap();
        }
    }
}
