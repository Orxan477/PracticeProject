using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PracticeProject.Configurations;
using PracticeProject.Models;

namespace PracticeProject.DAL
{
    public class AppDbContext:IdentityDbContext<ApplicationUser>
    {
        public AppDbContext(DbContextOptions<AppDbContext>options):base(options)
        {
        }
        public DbSet<Card> Cards { get; set; }
        public DbSet<Settings> Settings { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new CardConfiguration());
            modelBuilder.Entity<Card>().HasData(
                new Card { Id = 1, Name = "name", Content = "Information", Image = "team-2.jpg" });
            modelBuilder.Entity<Settings>().HasData(
                new Settings { Id = 1, Key = "Size", Value = "200" });
            base.OnModelCreating(modelBuilder);
        }
    }
}
