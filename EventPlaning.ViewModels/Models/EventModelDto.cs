using EventPlanning.Domain.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventPlanning.ViewModels.Models
{
    public class EventModelDto
    {
        public Guid Id { get; set; }

        public DateTime StartTime { get; set; }

        public long MaxCapacity { get; set; }

        public string EventName { get; set; }

        public string EventDescription { get; set; }

        public List<EventFieldDto> EventFields { get; set; }
    }
}
