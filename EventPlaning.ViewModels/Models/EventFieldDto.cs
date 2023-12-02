using EventPlanning.Domain.Enums;
using System.ComponentModel.DataAnnotations;
namespace EventPlanning.ViewModels.Models
{
    public class EventFieldDto
    {
        [Required]
        [DataType(DataType.Text)]
        [MaxLength(100)]
        public string Name { get; set; }

        [Required]
        [DataType(DataType.Text)]
        [MaxLength(1000)]
        public string Description { get; set; }

        [Required]
        public FieldType Type { get; set; }
    }
}
