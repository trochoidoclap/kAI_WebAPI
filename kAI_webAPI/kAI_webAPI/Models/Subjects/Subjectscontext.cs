using kAI_webAPI.Models.Subjects;
using Microsoft.EntityFrameworkCore;
namespace kAI_webAPI.Models.Subjects
{
    public class Subjectscontext : DbContext
    {
        public Subjectscontext(DbContextOptions<Subjectscontext> options) : base(options)
        {
        }
        public DbSet<Subjects> Subjects { get; set; } = null!;
        public DbSet<Subjects_group> Subjects_groups { get; set; } = null!;
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Subjects>().ToTable("subjects");
            modelBuilder.Entity<Subjects_group>().ToTable("subjects_group");
        }
    }
}
