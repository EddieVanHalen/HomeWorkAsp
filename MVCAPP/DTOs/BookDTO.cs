namespace MVCAPP.DTOs;

public class BookDTO
{
    public int Id { get; set; }

    public string Title { get; set; } = String.Empty;
    public string WriterName { get; set; } = String.Empty;
    public string Genre { get; set; } = String.Empty;

    public string? CoverImageUrl { get; set; }
}
