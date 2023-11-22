using EventPlanning.Domain.Enums;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventPlanning.DataAccess
{
    public class DbInitializer
    {
        public static async Task SeedRoles(RoleManager<IdentityRole<Guid>> roleManager)
        {
            if (roleManager.Roles.Any())
            {
                return;
            }

            foreach (var role in Enum.GetValues(typeof(Roles)))
            {
                await roleManager.CreateAsync(new IdentityRole<Guid>(Enum.GetName(typeof(Roles), role))).ConfigureAwait(false);
            }
        }
    }
}
