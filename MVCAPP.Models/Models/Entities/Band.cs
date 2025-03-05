namespace MVCAPP.Domain.Models.Entities;

public class Band
{
    public int Id { get; private set; }

    public string Title { get; private set; } = string.Empty;

    public Band()
    {
    }

    private Band(int id, string title)
    {
        Id = id;
        Title = title;
    }

    public static (Band Band, ICollection<string> errors) Create(int id, string title)
    {
        ICollection<string> errors = new List<string>();

        if (string.IsNullOrWhiteSpace(title) || title.Length < 3)
        {
            errors.Add("Title must be at least 3 characters.");
            return (new Band(), errors);
        }

        return (new Band(id, title), errors);
    }
}