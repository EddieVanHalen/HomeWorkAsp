namespace MVCAPP.DTOs;

public class BookDTO
{
    public int Id { get; set; }
    public int WriterId { get; set; }

    public string Title { get; set; } = String.Empty;
    public string Writer { get; set; } = String.Empty;
    public string Genre { get; set; } = String.Empty;

    public string? CoverImageUrl { get; set; }

    public void Test(){
      CoverImageUrl.
    }
}
