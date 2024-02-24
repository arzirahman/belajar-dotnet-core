using food_order_dotnet.Models;
using Microsoft.EntityFrameworkCore;

namespace food_order_dotnet.Data
{
    public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options) {
        public DbSet<User> Users { get; set; }

        public DbSet<Category> Categories { get; set; }

        public DbSet<Food> Foods { get; set; }

        public DbSet<FavoriteFood> FavoriteFoods { get; set; }

        public DbSet<Cart> Carts { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("users");
                entity.HasIndex(e => e.UserId, "users_pk").IsUnique();
            });

            modelBuilder.Entity<Category>(entity =>
            {
                entity.ToTable("categories");
                entity.HasIndex(e => e.CategoryId, "categories_pk").IsUnique();
            });

            modelBuilder.Entity<Food>(entity =>
            {
                entity.ToTable("foods");
                entity.HasIndex(e => e.FoodId, "foods_pk").IsUnique();
            });

            modelBuilder.Entity<FavoriteFood>(entity =>
            {
                entity.ToTable("favorite_foods");
                entity.HasKey(e => new { e.UserId, e.FoodId });
            });

            modelBuilder.Entity<Cart>(entity =>
            {
                entity.ToTable("carts");
                entity.HasIndex(e => e.CartId, "carts_pk").IsUnique();
            });
        }
    }
}