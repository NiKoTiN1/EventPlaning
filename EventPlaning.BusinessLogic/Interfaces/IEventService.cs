using EventPlanning.ViewModels.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventPlanning.BusinessLogic.Interfaces
{
    public interface IEventService
    {
        List<EventModelDto> GetAll();
        Task CreateEvent(CreateEventModelDto model);
    }
}
