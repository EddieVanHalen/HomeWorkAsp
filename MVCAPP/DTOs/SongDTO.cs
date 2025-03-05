namespace MVCAPP.DTOs;

public class SongDTO
{
    public int Id { get; set; }
    public int ArtistId { get; set; }
    public string Title { get; set; }
    public string ArtistName { get; set; }
    public string ImageUrl { get; set; }
}