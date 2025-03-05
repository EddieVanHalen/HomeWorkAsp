namespace MVCAPP.Domain.Models.Entities;

public class Album
{
    public Album(){}

    private Album(int id, int artistId, string title, string imageUrl)
    {
        Id = id;
        ArtistId = artistId;
        Title = title;
        ImageUrl = imageUrl;
    }

    public int Id { get; private set; }

    public int ArtistId { get; private set; }
	
    public string Title { get; private set; } = String.Empty;

    public string ImageUrl { get; set; } = null!;
    public static (Album album, ICollection<string> errors) Create(int id, int artistId, string title, string imageUrl)
    {
        ICollection<string> errors = new List<string>();

        if(string.IsNullOrEmpty(title) || title.Length < 3)
        {
            errors.Add("Album Name Must Be At Least 3 Symbols");

            return (new Album(), errors);
        }
        
        if(string.IsNullOrEmpty(imageUrl))
        {
            errors.Add("Album Name Must Be At Least 3 Symbols");

            return (new Album(), errors);
        }

        return (new Album(id, artistId, title, imageUrl), errors);
    }
}