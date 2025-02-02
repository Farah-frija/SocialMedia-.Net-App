using Core.Domain.Entities;

using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Context
{
    public class ApplicationDbContext : IdentityDbContext<User>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        // DbSet for FriendRequest
        public DbSet<Follow> Follows { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder); // Important for Identity!

            // Configure relationships for FriendRequest
            modelBuilder.Entity<Follow>()
         .HasOne(f => f.Sender)
         .WithMany(u => u.Followings)  // A single list, both sent and received follows
         .HasForeignKey(f => f.SenderId)
         .IsRequired();

            modelBuilder.Entity<Follow>()
                .HasOne(f => f.Receiver)
                .WithMany(u => u.Followers)  // The same list here, but refers to received follows
                .HasForeignKey(f => f.ReceiverId)
                .IsRequired();
        }
    }
}

