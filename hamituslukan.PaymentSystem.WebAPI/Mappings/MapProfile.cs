using AutoMapper;
using hamituslukan.PaymentSystem.Dto.Concrete;
using hamituslukan.PaymentSystem.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace hamituslukan.PaymentSystem.WebAPI.Mappings
{
    public class MapProfile : Profile
    {
        public MapProfile()
        {
            CreateMap<ApplicationUser, ApplicationUserDto>()
                .ForMember(d => d.Password, s => s.Ignore())
                .ForMember(d => d.Email, s => s.MapFrom(s => s.UserName)).ForMember(d => d.Subscribers, s => s.Ignore()).ReverseMap();
            CreateMap<SubscriberType, SubscriberTypeDto>().ReverseMap();
            CreateMap<Deposit, DepositDto>().ReverseMap();
            CreateMap<Subscriber, SubscriberDto>().ReverseMap();
            CreateMap<Invoice, InvoiceDto>().ForMember(d => d.Subscriber, s => s.Ignore()).ReverseMap();
        }
    }
}