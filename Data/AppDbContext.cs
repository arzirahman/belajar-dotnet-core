using food_order_dotnet.Models;
using Microsoft.EntityFrameworkCore;

namespace food_order_dotnet.Data
{
    public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options) {
        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("users");
                entity.HasIndex(e => e.UserId, "users_pk").IsUnique();
            });
        }
    }
}