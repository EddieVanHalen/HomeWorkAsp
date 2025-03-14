using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MVCAPP.Areas.Admin.Models;

public class BookRequest
{
    public int Id { get; set; }

    [Required]
    [StringLength(
        100,
        MinimumLength = 3,
        ErrorMessage = "Title must be between 3 and 100 characters"
    )]
    public string? Title { get; set; }

    [Required]
    public string? Genre { get; set; }

    [Required]
    public string? AuthorFullName { get; set; }

    public string? CoverImageUrl { get; set; }

    [NotMapped]
    public IFormFile? ImageFile { get; set; }

    public BookRequest() { }

    public BookRequest(
        int id,
        string title,
        string genre,
        string authorFullName,
        string? coverImageUrl,
        IFormFile? imageFile = null
    )
    {
        Id = id;
        CoverImageUrl = coverImageUrl;
        ImageFile = imageFile;
        AuthorFullName = authorFullName;
        Title = title;
        Genre = genre;
    }
}
