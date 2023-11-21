using System;
using System.Collections.Generic;
using System.Text;

namespace EventPlanning.Domain.Models
{
    public class EventModel : IBaseModel
    {
        public Guid Id { get; set; }

        public DateTime StartTime { get; set; }

        public long MaxCapacity { get; set; }

        public List<EventField> EventFields { get; set; }
    }
}
