﻿using kAI_webAPI.Models;
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
                entity.HasKey(e => e.Id_subjects);
                entity.Property(e => e.subject_name).HasColumnName("subject");
                entity.Property(e => e.Type).HasColumnName("type");
            });
            builder.Entity<SubjectsGroup>(entity =>
            {
                entity.ToTable("subjects_group");
                entity.HasKey(e => e.Id_subgroup);

                entity.HasOne(sg => sg.Subject1Navigation)
                .WithMany()
                .HasForeignKey(sg => sg.Subject1)
                .OnDelete(DeleteBehavior.Restrict);
                entity.HasOne(sg => sg.Subject2Navigation)
                .WithMany()
                .HasForeignKey(sg => sg.Subject2)
                .OnDelete(DeleteBehavior.Restrict);
                entity.HasOne(sg => sg.Subject3Navigation)
                .WithMany()
                .HasForeignKey(sg => sg.Subject3)
                .OnDelete(DeleteBehavior.Restrict);
                entity.HasOne(sg => sg.Subject4Navigation)
                .WithMany()
                .HasForeignKey(sg => sg.Subject4)
                .OnDelete(DeleteBehavior.Restrict);
                entity.HasOne(sg => sg.Subject5Navigation)
                .WithMany()
                .HasForeignKey(sg => sg.Subject5)
                .OnDelete(DeleteBehavior.Restrict);
            });
        }

    }
}
