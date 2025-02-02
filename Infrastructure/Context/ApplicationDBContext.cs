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
        public DbSet<Post> Posts { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<PostPhoto> PostPhotos { get; set; } // Register PostPhoto

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder); // Important pour Identity!
            modelBuilder.Entity<Comment>()
       .HasOne(c => c.User)
       .WithMany()
       .HasForeignKey(c => c.UserId)
       .OnDelete(DeleteBehavior.Restrict); // Change to Restrict to avoid cascading deletes

            modelBuilder.Entity<Comment>()
                .HasOne(c => c.Post)
                .WithMany(p => p.Comments)
                .HasForeignKey(c => c.PostId)
                .OnDelete(DeleteBehavior.Cascade); // Keep cascading deletes for Post

            modelBuilder.Entity<PostPhoto>()
          .HasOne(pp => pp.Post)
          .WithMany(p => p.Photos)
          .HasForeignKey(pp => pp.PostId);
        }
        
    }
}
