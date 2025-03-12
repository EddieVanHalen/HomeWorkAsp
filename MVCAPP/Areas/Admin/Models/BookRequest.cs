using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MVCAPP.Areas.Admin.Models;

public class BookRequest
{
    public int Id { get; set; }
    
    [Required]
    public string Title { get; set; }
    
    [Required]
    public string Genre { get; set; }
    
    public string? CoverImageUrl { get; set; }
    
    [NotMapped]
    public IFormFile? ImageFile { get; set; }

    public BookRequest(int id, string title, string genre, string? coverImageUrl, IFormFile? imageFile)
    {
        Id = id;
        CoverImageUrl = coverImageUrl;
        ImageFile = imageFile;
        Title = title;
        Genre = genre;
    }
}