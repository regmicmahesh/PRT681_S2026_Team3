using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace TheatreAdmin.Models;

public class Movie
{
    public int Id { get; set; }

    [Required]
    [StringLength(100, MinimumLength = 2)]
    public string Name { get; set; } = string.Empty;

    [Required]
    [DataType(DataType.Date)]
    [Display(Name = "Release date")]
    public DateTime ReleaseDate { get; set; }

    [Required]
    [StringLength(80, MinimumLength = 2)]
    public string Director { get; set; } = string.Empty;

    [Required]
    [EmailAddress]
    [Display(Name = "Contact email")]
    public string ContactEmail { get; set; } = string.Empty;

    [Required]
    public MovieLanguage Language { get; set; }

    [Range(1, int.MaxValue, ErrorMessage = "Please select a category.")]
    [Display(Name = "Category")]
    public int CategoryId { get; set; }

    [ValidateNever]
    public Category Category { get; set; } = null!;
}
