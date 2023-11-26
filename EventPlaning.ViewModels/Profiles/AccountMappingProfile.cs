using AutoMapper;
using EventPlanning.Domain.Models;
using EventPlanning.ViewModels.Models;

namespace EventPlanning.ViewModels.Profiles
{
    public class AccountMappingProfile : Profile
    {
        public AccountMappingProfile()
        {
            CreateMap<RegisterModel, Account>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => Guid.NewGuid()))
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
                .ForMember(dest => dest.PhoneNumber, opt => opt.MapFrom(src => src.PhoneNumber))
                .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.UserName));

            CreateMap<RegisterModel, Guest>()
                 .ForMember(dest => dest.BirthDate, opt => opt.MapFrom(src => src.BirthDate))
                 .ForMember(dest => dest.Account, opt => opt.MapFrom(src => src));

            CreateMap<RegisterModel, Creator>()
                 .ForMember(dest => dest.OrganizationName, opt => opt.MapFrom(src => src.OrganizationName))
                 .ForMember(dest => dest.Website, opt => opt.MapFrom(src => src.OrganizationName))
                 .ForMember(dest => dest.Account, opt => opt.MapFrom(src => src));
        }
    }
}
