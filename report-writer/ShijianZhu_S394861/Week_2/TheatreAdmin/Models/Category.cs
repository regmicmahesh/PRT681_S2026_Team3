using System.ComponentModel.DataAnnotations;

namespace TheatreAdmin.Models;

public class Category
{
    public int Id { get; set; }

    [Required]
    [StringLength(50, MinimumLength = 2)]
    public string Name { get; set; } = string.Empty;

    [Required]
    [StringLength(10, MinimumLength = 2)]
    [RegularExpression("^[A-Z0-9-]+$", ErrorMessage = "Code must use uppercase letters, numbers, or hyphens.")]
    public string Code { get; set; } = string.Empty;

    public ICollection<Movie> Movies { get; set; } = new List<Movie>();
}
