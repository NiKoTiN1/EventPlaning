using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventPlanning.ViewModels.Models
{
    public class RegisterModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required]
        public string UserName { get; set; }

        [Required]
        public string PhoneNumber { get; set; }

        [DataType(DataType.Date)]
        public DateTime? BirthDate { get; set; }

        [MinLength(5)]
        [MaxLength(100)]
        public string? OrganizationName { get; set; }

        [MinLength(5)]
        [MaxLength(100)]
        public string? Website { get; set; }

        [Required]
        public string UserType { get; set; }
    }
}
