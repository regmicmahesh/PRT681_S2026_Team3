using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TheatreAdmin.Controllers;
using TheatreAdmin.Data;
using TheatreAdmin.Models;

namespace TheatreAdmin.Tests;

public class CrudControllerTests
{
    [Fact]
    public void MovieCreate_ShowsAvailableCategories()
    {
        using var context = CreateContext();
        var controller = new MoviesController(context);

        var result = controller.Create();

        Assert.IsType<ViewResult>(result);
        var options = Assert.IsType<SelectList>(controller.ViewData["CategoryId"]);
        Assert.Equal(3, options.Count());
    }

    [Fact]
    public async Task MovieCreate_PersistsValidMovie()
    {
        using var context = CreateContext();
        var controller = new MoviesController(context);
        var movie = new Movie
        {
            Name = "The Farewell",
            ReleaseDate = new DateTime(2019, 1, 25),
            Director = "Lulu Wang",
            ContactEmail = "contact@theatre.example",
            Language = MovieLanguage.English,
            CategoryId = 2
        };

        var result = await controller.Create(movie);

        Assert.IsType<RedirectToActionResult>(result);
        Assert.True(await context.Movies.AnyAsync(item => item.Name == "The Farewell"));
    }

    [Fact]
    public async Task CategoryDelete_IsBlockedWhenMoviesUseTheCategory()
    {
        using var context = CreateContext();
        var controller = new CategoriesController(context);

        var result = await controller.DeleteConfirmed(1);

        Assert.IsType<ViewResult>(result);
        Assert.True(await context.Categories.AnyAsync(category => category.Id == 1));
        Assert.False(controller.ModelState.IsValid);
    }

    private static TheatreAdminContext CreateContext()
    {
        var options = new DbContextOptionsBuilder<TheatreAdminContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;

        var context = new TheatreAdminContext(options);
        context.Database.EnsureCreated();
        return context;
    }
}
