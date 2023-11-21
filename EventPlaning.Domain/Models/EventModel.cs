using System;
using System.Collections.Generic;
using System.Text;

namespace EventPlaning.Domain.Models
{
    public class EventModel : IBaseModel
    {
        public Guid Id { get; set; }

        public DateTime StartTime { get; set; }

        public long MaxCapacity { get; set; }
    }
}
