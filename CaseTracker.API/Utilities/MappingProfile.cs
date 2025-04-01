using AutoMapper;
using CaseTracker.Data.ContextModels;
using CaseTracker.Data.FormModels;

namespace CaseTracker.API.Utilities
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<RegisterFM, User>();
            CreateMap<CaseFM, Case>().ReverseMap();
            CreateMap<ClientFM, Client>().ReverseMap();
        }
    }
}
