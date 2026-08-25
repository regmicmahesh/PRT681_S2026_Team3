using System.ComponentModel.DataAnnotations;
using TheatreAdmin.Models;

namespace TheatreAdmin.Tests;

public class ModelValidationTests
{
    [Fact]
    public void CompleteMovie_PassesValidation()
    {
        var results = Validate(new Movie
        {
            Name = "Spirited Away",
            ReleaseDate = new DateTime(2001, 7, 20),
            Director = "Hayao Miyazaki",
            ContactEmail = "contact@theatre.example",
            Language = MovieLanguage.Japanese,
            CategoryId = 1
        });

        Assert.Empty(results);
    }

    [Fact]
    public void InvalidMovie_ReturnsUsefulValidationErrors()
    {
        var results = Validate(new Movie
        {
            Name = string.Empty,
            ReleaseDate = new DateTime(2020, 1, 1),
            Director = "A",
            ContactEmail = "not-an-email",
            Language = MovieLanguage.English,
            CategoryId = 0
        });

        Assert.Contains(results, result => result.MemberNames.Contains(nameof(Movie.Name)));
        Assert.Contains(results, result => result.MemberNames.Contains(nameof(Movie.Director)));
        Assert.Contains(results, result => result.MemberNames.Contains(nameof(Movie.ContactEmail)));
        Assert.Contains(results, result => result.MemberNames.Contains(nameof(Movie.CategoryId)));
    }

    [Fact]
    public void LowercaseCategoryCode_FailsValidation()
    {
        var results = Validate(new Category { Name = "Comedy", Code = "com" });

        Assert.Contains(results, result => result.MemberNames.Contains(nameof(Category.Code)));
    }

    private static List<ValidationResult> Validate(object model)
    {
        var results = new List<ValidationResult>();
        Validator.TryValidateObject(model, new ValidationContext(model), results, true);
        return results;
    }
}
