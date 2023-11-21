using System;
using System.Collections.Generic;
using System.Security.Principal;
using System.Text;

namespace EventPlaning.Domain.Models
{
    internal class Account : IdentityUser, IEntity
    {
        public Guid Id { get; set; }
    }
}
