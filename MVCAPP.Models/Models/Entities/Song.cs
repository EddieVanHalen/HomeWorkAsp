namespace MVCAPP.Domain.Models.Entities;

public class Song
{
    public int Id { get; private set; }
    
    public int AlbumId { get; private set; }
    
    public string Title { get; private set; } = String.Empty;

    public Song()
    {
        
    }
    
    private Song(int id, int albumId, string title)
    {
        Id = id;
        AlbumId = albumId;
        Title = title;
    }

    public static (Song song, ICollection<string> errors) Create(int id, int albumId, string title)
    {
        ICollection<string> errors = new List<string>();
        
        if (string.IsNullOrWhiteSpace(title) || title.Length < 3)
        {
            errors.Add("Title must be at least 3 characters.");

            return (new Song(), errors);
        }

        return (new Song(id, albumId, title), errors);
    }
}