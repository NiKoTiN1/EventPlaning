using AutoMapper;
using EventPlanning.Domain.Models;
using EventPlanning.ViewModels.Models;

namespace EventPlanning.ViewModels.Profiles
{
    public class TokenMappingProfile : Profile
    {
        public TokenMappingProfile()
        {
            CreateMap<RefreshToken, TokenViewModel>()
                    .ForMember(dest => dest.RefreshToken, opt => opt.MapFrom(src => src.Token));
        }
    }
}
