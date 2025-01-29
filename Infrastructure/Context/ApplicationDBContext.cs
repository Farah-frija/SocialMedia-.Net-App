using Core.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext()
        {
            
        }
        public DbSet<User> Users { get; set; }
        public DbSet<PrivacySettings> PrivacySettings { get; set; }
        public DbSet<Story> Stories { get; set; }
        public DbSet<StoryComment> StoryComments { get; set; }
        public DbSet<StoryLike> StoryLikes { get; set; }
        
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder); // Important pour Identity!
            
            modelBuilder.Entity<User>()
            .HasOne(u => u.PrivacySettings) // User has one PrivacySettings
            .WithOne(p => p.User)           // PrivacySettings belongs to one User
            .HasForeignKey<PrivacySettings>(p => p.UserId) // FK in PrivacySettings
            .IsRequired();                  // Ensure PrivacySettings must have a User

            modelBuilder.Entity<Story>()
             .HasOne(s => s.User)
             .WithMany(u => u.Stories)
             .HasForeignKey(s => s.UserId)
            .OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<StoryComment>()
            .HasOne(c => c.User)
            .WithMany() // No navigation property in User
            .HasForeignKey(c => c.UserId)
            .OnDelete(DeleteBehavior.NoAction);

            // Prevent cascading deletes for StoryComments → Stories
            modelBuilder.Entity<StoryComment>()
                .HasOne(c => c.Story)
                .WithMany(s => s.Comments)
                .HasForeignKey(c => c.StoryId)
                .OnDelete(DeleteBehavior.Cascade); // Only allow cascade on one path
            modelBuilder.Entity<StoryLike>()
            .HasKey(sl => sl.Id);

            modelBuilder.Entity<StoryLike>()
                .HasOne(sl => sl.Story)
                .WithMany(s => s.Likes)
                .HasForeignKey(sl => sl.StoryId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<StoryLike>()
                .HasOne(sl => sl.User)
                .WithMany()
                .HasForeignKey(sl => sl.UserId)
                .OnDelete(DeleteBehavior.NoAction);
        }
        
    }
}
