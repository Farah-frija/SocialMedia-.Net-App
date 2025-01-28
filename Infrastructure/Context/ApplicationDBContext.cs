using Core.Domain.Entities;

using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Context
{
    public class ApplicationDbContext : IdentityDbContext<User>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        // DbSet for FriendRequest
        public DbSet<FriendRequest> FriendRequests { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder); // Important for Identity!

            // Configure relationships for FriendRequest
            modelBuilder.Entity<User>()
                .HasMany(u => u.SentFriendRequests)
                .WithOne(u=>u.Sender)
                .HasForeignKey(u => u.SenderId)
              
                .IsRequired();

            modelBuilder.Entity<User>()
                .HasMany(u => u.ReceivedFriendRequests)
                .WithOne(u => u.Receiver)
                .HasForeignKey(u => u.ReceiverId)
              
                .IsRequired();
        }
    }
}

