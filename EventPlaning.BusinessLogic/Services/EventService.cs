using AutoMapper;
using EventPlanning.BusinessLogic.Interfaces;
using EventPlanning.DataAccess.Interfaces;
using EventPlanning.Domain.Models;
using EventPlanning.ViewModels.Models;

namespace EventPlanning.BusinessLogic.Services
{
    public class EventService : IEventService
    {
        private readonly IEventRepository _eventRepository;
        private readonly IMapper _mapper;

        public EventService(
            IEventRepository eventRepository,
            IMapper mapper
            )
        {
            _eventRepository = eventRepository;
            _mapper = mapper;
        }

        //public async Task GetAll()
        //{
        //    return await _eventRepository.Get(e => true, new string[] { "EventField" })
        //}

        public async Task CreateEvent(CreateEventModelDto model)
        {
            var eventModel = _mapper.Map<EventModel>(model);

            await _eventRepository.Add(eventModel);
        }
    }
}
