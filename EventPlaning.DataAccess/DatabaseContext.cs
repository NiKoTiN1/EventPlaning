using EventPlanning.Domain.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace EventPlanning.DataAccess
{
    public class DatabaseContext : IdentityDbContext<Account, IdentityRole<Guid>, Guid>
    {
        public DbSet<EventModel> EventModels { get; set; }

        public DbSet<EventField> EventFields { get; set; }

        public DbSet<RefreshToken> RefreshTokens { get; set; }

        public DbSet<Guest> Guests { get; set; }

        public DbSet<Creator> Creators { get; set; }

        public DatabaseContext(DbContextOptions<DatabaseContext> options)
            : base(options)
        {
        }
    }
}
