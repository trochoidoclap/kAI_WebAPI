using kAI_webAPI.Models.Subjects;
using kAI_webAPI.Models.User;
using kAI_webAPI.Models.Question;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace kAI_webAPI.Data
{
    public class ApplicationDBContext : DbContext
    {
        public ApplicationDBContext(DbContextOptions<ApplicationDBContext> dbContextOptions)
            : base(dbContextOptions)
        { } 
        public DbSet<User> Users { get; set; } = null!;
        public DbSet<Question> Questions { get; set; } = null!;
        public DbSet<Subjects> Subjects { get; set; } = null!;
        public DbSet<Subjects_group> Subjects_groups { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Subjects>().ToTable("subjects");
            modelBuilder.Entity<Subjects_group>().ToTable("subjects_group");
        }
    }
}
