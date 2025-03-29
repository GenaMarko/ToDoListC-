using Microsoft.EntityFrameworkCore;

namespace ToDoList.Models
{
    public class ToDoContext : DbContext
    {
        public ToDoContext(DbContextOptions<ToDoContext> options) : base(options) { }

        public DbSet<ToDoModel> toDos { get; set; } = null!;

        public DbSet<CategoryModel> category { get; set; } = null!;

        public DbSet<StatusModel> status { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CategoryModel>().HasData(
                new CategoryModel { categoryId = "work", categoryName = "Work"},
                new CategoryModel { categoryId = "home", categoryName = "Home" },
                new CategoryModel { categoryId = "ex", categoryName = "Exercise" },
                new CategoryModel { categoryId = "shop", categoryName = "Shopping" },
                new CategoryModel { categoryId = "call", categoryName = "Contact" }
            );
        }
    }
}
