namespace MVCAPP.Domain.Models.Entities;

public class Writer
{
    public int Id { get; private set; }

    public string Fullname { get; private set; } = string.Empty;

    public string? ImageUrl { get; private set; }

    public Writer()
    {
    }

    private Writer(int id, string fullname, string? imageUrl)
    {
        Id = id;
        Fullname = fullname;
        ImageUrl = imageUrl;
    }

    public static (Writer writer, ICollection<string> errors) Create(
        int id,
        string fullname,
        string? imageUrl
    )
    {
        ICollection<string> errors = new List<string>();

        if (string.IsNullOrWhiteSpace(fullname))
        {
            errors.Add("Nickname Is Empty");

            return (new Writer(), errors);
        }

        return (new Writer(id, fullname, imageUrl), errors);
    }
}