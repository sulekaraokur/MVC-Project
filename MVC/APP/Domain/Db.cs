using Microsoft.EntityFrameworkCore;

namespace APP.Domain
{
    public class Db : DbContext
    {
        public DbSet<User> Users { get; set; }

        public DbSet<Role> Roles { get; set; }

        public DbSet<UserRole> UserRoles { get; set; }

        public DbSet<Status> Statuses { get; set; }

        public DbSet<Blog> Blogs { get; set; }

        public DbSet<Tag> Tags { get; set; }

        public DbSet<BlogTag> BlogTags { get; set; }

        public Db(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Deleting a user keeps their blogs and clears the blogs' UserId.
            modelBuilder.Entity<Blog>()
                .HasOne(blog => blog.User)
                .WithMany(user => user.Blogs)
                .HasForeignKey(blog => blog.UserId)
                .OnDelete(DeleteBehavior.SetNull);
        }
    }
}
