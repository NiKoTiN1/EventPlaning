using System.ComponentModel.DataAnnotations;

namespace EventPlanning.Domain.Models
{
    public class EventModel : IBaseModel
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        [DataType(DataType.DateTime)]
        public DateTime StartTime { get; set; }

        [Required]
        public long MaxCapacity { get; set; }

        [Required]
        [MaxLength(100)]
        public string EventName { get; set; }

        [Required]
        [MaxLength(1000)]
        public string EventDescriptions { get; set; }

        public List<EventField> EventFields { get; set; }
    }
}
