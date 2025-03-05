namespace MVCAPP.Domain.Models.Entities;

public class Artist
{
    public int Id { get; private set; }

    public int? BandId { get; private set; } = null;

    public string Name { get; private set; } = string.Empty;
    
    public string ImageUrl { get; set; } = null!;

    public Artist()
    {
    }

    private Artist(int id, int? bandId, string name, string imageUrl)
    {
        Id = id;
        BandId = bandId;
        Name = name;
        ImageUrl = imageUrl;
    }

    public static (Artist artist, ICollection<string> errors) Create(int id, int? bandId, string name, string imageUrl)
    {
        ICollection<string> errors = new List<string>();

        if (string.IsNullOrEmpty(name) || name.Length < 3)
        {
            errors.Add("Name Must Be At Least 3 Symbols.");
            return (new Artist(), errors);
        }

        if (string.IsNullOrEmpty(imageUrl))
        {
            errors.Add("Image Url Is Empty");
            return (new Artist(), errors);
        }

        return (new Artist(id, bandId, name, imageUrl), errors);
    }
}