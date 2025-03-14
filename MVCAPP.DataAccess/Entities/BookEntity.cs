namespace MVCAPP.DataAccess.Entities;

public class BookEntity
{
    public int Id { get; set; }

    public string Title { get; set; } = string.Empty;

    public string AuthorFullName { get; set; } = string.Empty;

    public string Genre { get; set; } = string.Empty;

    public string? CoverImageUrl { get; set; }
}
