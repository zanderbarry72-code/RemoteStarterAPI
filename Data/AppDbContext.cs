using Microsoft.EntityFrameworkCore;
using RemoteStarterAPI.Models;

namespace RemoteStarterAPI.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Seed initial users
            modelBuilder.Entity<User>().HasData(
                new User { Id = 1, Name = "Alice Johnson", Email = "alice@example.com" },
                new User { Id = 2, Name = "Bob Smith", Email = "bob@example.com" },
                new User { Id = 3, Name = "Charlie Brown", Email = "charlie@example.com" }
            );
        }
    }
}