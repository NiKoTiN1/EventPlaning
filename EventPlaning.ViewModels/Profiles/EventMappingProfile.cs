using AutoMapper;
using EventPlanning.Domain.Models;
using EventPlanning.ViewModels.Models;

namespace EventPlanning.ViewModels.Profiles
{
    public class EventMappingProfile : Profile
    {
        public EventMappingProfile()
        {
            CreateMap<EventFieldDto, EventField>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => Guid.NewGuid()))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
                .ForMember(dest => dest.Type, opt => opt.MapFrom(src => src.FieldType));

            CreateMap<CreateEventModelDto, EventModel>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => Guid.NewGuid()))
                .ForMember(dest => dest.MaxCapacity, opt => opt.MapFrom(src => src.MaxCapacity))
                .ForMember(dest => dest.EventName, opt => opt.MapFrom(src => src.EventName))
                .ForMember(dest => dest.EventDescription, opt => opt.MapFrom(src => src.EventDescription))
                .ForMember(dest => dest.StartTime, opt => opt.MapFrom(src => src.StartTime))
                .ForMember(dest => dest.EventFields, opt => opt.MapFrom(src => src.EventFields));

            CreateMap<EventField, EventFieldDto>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => Guid.NewGuid()))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
                .ForMember(dest => dest.FieldType, opt => opt.MapFrom(src => src.Type));

            CreateMap<EventModel, EventModelDto>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => Guid.NewGuid()))
                .ForMember(dest => dest.MaxCapacity, opt => opt.MapFrom(src => src.MaxCapacity))
                .ForMember(dest => dest.EventName, opt => opt.MapFrom(src => src.EventName))
                .ForMember(dest => dest.EventDescription, opt => opt.MapFrom(src => src.EventDescription))
                .ForMember(dest => dest.StartTime, opt => opt.MapFrom(src => src.StartTime))
                .ForMember(dest => dest.EventFields, opt => opt.MapFrom(src => src.EventFields));
        }
    }
}
