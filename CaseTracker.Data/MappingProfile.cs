using AutoMapper;
using CaseTracker.Data.ContextModels;
using CaseTracker.Data.FormModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CaseTracker.Data
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<RegisterFM, ApplicationUser>().ReverseMap();
            CreateMap<CaseFM, Case>().ReverseMap();
            CreateMap<ClientFM, Client>().ReverseMap();
            CreateMap<LawyerFM, Lawyer>().ReverseMap();
            CreateMap<CaseDocumentFM, CaseDocument>().ReverseMap();
            CreateMap<PaymentFM, Payment>().ReverseMap();
            CreateMap<SubscriptionPackageFM, SubscriptionPackage>().ReverseMap();
            CreateMap<UserSubscription, UserSubscription>().ReverseMap();
            CreateMap<ClientFM, Client>().ReverseMap();
            CreateMap<ClientFM, Client>().ReverseMap();
            // Mapping from UserFM to ApplicationUser
            CreateMap<UserFM, ApplicationUser>()
                .ForMember(dest => dest.Firstname, opt => opt.MapFrom(src => src.Firstname))
                .ForMember(dest => dest.Lastname, opt => opt.MapFrom(src => src.Lastname))
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.EmailId))
                .ForMember(dest => dest.PhoneNumber, opt => opt.MapFrom(src => src.MobileNo))
                .ForMember(dest => dest.Address, opt => opt.MapFrom(src => src.Address))
                .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.Username))
                .ForMember(dest => dest.State, opt => opt.MapFrom(src => src.City));

            // Mapping from ApplicationUser to Lawyer
            CreateMap<ApplicationUser, Lawyer>()
                .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.Id));

            // Mapping from ApplicationUser to Client
            CreateMap<ApplicationUser, Client>()
                .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.Id));
        }
    }
}
