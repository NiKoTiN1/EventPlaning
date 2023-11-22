using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventPlanning.ViewModels.Models
{
    public class RegisterCreatorViewModel : RegisterBaseModel
    {
        [Required]
        [MinLength(5)]
        [MaxLength(100)]
        public string OrganizationName { get; set; }

        [Required]
        [MinLength(5)]
        [MaxLength(100)]
        public string Website { get; set; }
    }
}
