using AutoMapper;
using CMS.Data.ContextModels;
using CMS.Data.FormModels;

namespace CMS.API.Utilities
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<RegisterFM, User>();
        }
    }
}
