using Microsoft.EntityFrameworkCore;
using TheatreAdmin.Models;

namespace TheatreAdmin.Data;

public class TheatreAdminContext(DbContextOptions<TheatreAdminContext> options)
    : DbContext(options)
{
    public DbSet<Movie> Movies => Set<Movie>();
    public DbSet<Category> Categories => Set<Category>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Category>()
            .HasIndex(category => category.Code)
            .IsUnique();

        modelBuilder.Entity<Movie>()
            .HasOne(movie => movie.Category)
            .WithMany(category => category.Movies)
            .HasForeignKey(movie => movie.CategoryId)
            .IsRequired()
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Category>().HasData(
            new Category { Id = 1, Name = "Action", Code = "ACT" },
            new Category { Id = 2, Name = "Drama", Code = "DRM" },
            new Category { Id = 3, Name = "Horror", Code = "HOR" }
        );

        modelBuilder.Entity<Movie>().HasData(
            new Movie
            {
                Id = 1,
                Name = "Seven Samurai",
                ReleaseDate = new DateTime(1954, 4, 26),
                Director = "Akira Kurosawa",
                ContactEmail = "programming@theatre.example",
                Language = MovieLanguage.Japanese,
                CategoryId = 1
            },
            new Movie
            {
                Id = 2,
                Name = "Farewell My Concubine",
                ReleaseDate = new DateTime(1993, 1, 1),
                Director = "Chen Kaige",
                ContactEmail = "programming@theatre.example",
                Language = MovieLanguage.Chinese,
                CategoryId = 2
            }
        );
    }
}
