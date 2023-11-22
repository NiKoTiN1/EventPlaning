using AutoMapper;
using EventPlanning.Domain.Models;
using EventPlanning.ViewModels.Models;

namespace EventPlanning.ViewModels.Profiles
{
    public class AccountMappingProfile : Profile
    {
        public AccountMappingProfile()
        {
            CreateMap<RegisterGuestViewModel, Account>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => Guid.NewGuid()))
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
                .ForMember(dest => dest.PhoneNumber, opt => opt.MapFrom(src => src.PhoneNumber))
                .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.UserName));

            CreateMap<RegisterGuestViewModel, Guest>()
                 .ForMember(dest => dest.BirthDate, opt => opt.MapFrom(src => src.BirthDate))
                 .ForMember(dest => dest.Account, opt => opt.MapFrom(src => src));
        }
    }
}
