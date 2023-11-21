using EventPlanning.Domain.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace EventPlanning.DataAccess
{
    public class DatabaseContext : IdentityDbContext<Account, IdentityRole<Guid>, Guid>
    {
        public DbSet<EventModel> EventModels { get; set; }

        public DbSet<EventField> EventFields { get; set; }

        public DbSet<RefreshToken> RefreshTokens { get; set; }

        public DatabaseContext(DbContextOptions<DatabaseContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }
    }
}
