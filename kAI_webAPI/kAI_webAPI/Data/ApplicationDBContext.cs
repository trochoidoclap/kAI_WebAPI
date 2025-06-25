using kAI_webAPI.Models;
using kAI_webAPI.Models.Question;
using kAI_webAPI.Models.Subjects;
using kAI_webAPI.Models.Transcript;
using kAI_webAPI.Models.User;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace kAI_webAPI.Data
{
    public class ApplicationDBContext : DbContext
    {
        public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options)
            : base(options)
        {
        }

        public DbSet<RefreshToken> RefreshTokens { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Question> Questions { get; set; }
        public DbSet<Questions_type> QuestionsTypes { get; set; }
        public DbSet<Subjects> Subjects { get; set; }
        public DbSet<SubjectsGroup> SubjectsGroups { get; set; }
        public DbSet<Transcript> Transcript { get; set; }
        public DbSet<Remark> Remark { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<Subjects>(entity =>
            {
                entity.ToTable("subjects");
                entity.HasKey(e => e.id_subjects);
                entity.Property(e => e.subject_name).HasColumnName("subject");
                entity.Property(e => e.type).HasColumnName("type");
            });
            builder.Entity<SubjectsGroup>(entity =>
            {
                entity.ToTable("subjects_group");
                entity.HasKey(e => e.id_subgroup);

                entity.HasOne(sg => sg.subject1Navigation)
                .WithMany()
                .HasForeignKey(sg => sg.subject1)
                .OnDelete(DeleteBehavior.Restrict);
                entity.HasOne(sg => sg.subject2Navigation)
                .WithMany()
                .HasForeignKey(sg => sg.subject2)
                .OnDelete(DeleteBehavior.Restrict);
                entity.HasOne(sg => sg.subject3Navigation)
                .WithMany()
                .HasForeignKey(sg => sg.subject3)
                .OnDelete(DeleteBehavior.Restrict);
                entity.HasOne(sg => sg.subject4Navigation)
                .WithMany()
                .HasForeignKey(sg => sg.subject4)
                .OnDelete(DeleteBehavior.Restrict);
                entity.HasOne(sg => sg.subject5Navigation)
                .WithMany()
                .HasForeignKey(sg => sg.subject5)
                .OnDelete(DeleteBehavior.Restrict);
            });
        }

    }
}
