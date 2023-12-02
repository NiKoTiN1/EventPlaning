using EventPlanning.Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace EventPlanning.ViewModels.Models
{
    public class CreateEventModelDto
    {
        [Required]
        [MaxLength(100)]
        public string EventName { get; set; }

        [Required]
        [MaxLength(1000)]
        public string EventDescription { get; set; }

        [Required]
        [DataType(DataType.DateTime)]
        public DateTime StartTime { get; set; }

        [Required]
        public long MaxCapacity { get; set; }

        public List<EventFieldDto> EventFields { get; set; }
    }
}
