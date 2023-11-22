using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
namespace EventPlanning.Domain.Models
{
    public class Account : IdentityUser<Guid>, IBaseModel
    {
        public RefreshToken? RefreshToken { get; set; }
    }
}
