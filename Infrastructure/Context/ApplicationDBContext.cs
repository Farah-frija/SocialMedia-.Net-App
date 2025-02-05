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
        public DbSet<Message> Messages { get; set; }
        public DbSet<ChatRoom> ChatRooms { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<PostPhoto> PostPhotos { get; set; } // Register PostPhoto
        public DbSet<Like> Likes { get; set; }

        public DbSet<Story> Stories { get; set; }
        public DbSet<StoryComment> StoryComments { get; set; }
        public DbSet<StoryLike> StoryLikes { get; set; }
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

            modelBuilder.Entity<Like>()
                .HasOne(l => l.Post)
                .WithMany(p => p.Likes)
                .HasForeignKey(l => l.PostId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Like>()
                .HasOne(l => l.User)
                .WithMany(u => u.Likes)
                .HasForeignKey(l => l.UserId)
                .OnDelete(DeleteBehavior.NoAction);

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

            modelBuilder.Entity<User>()
      .Property(u => u.IsPrivateProfile)
      .HasDefaultValue(true);

            modelBuilder.Entity<User>()
                .Property(u => u.ProfilePictureUrl)
                .HasMaxLength(500);
        }
    }
}

