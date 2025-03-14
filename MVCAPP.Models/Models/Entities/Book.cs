namespace MVCAPP.Domain.Models.Entities;

public class Book
{
    public int Id { get; private set; }

    public string Title { get; private set; } = string.Empty;
    public string AuthorFullName { get; private set; } = string.Empty;

    public string Genre { get; private set; } = string.Empty;

    public string? CoverImageUrl { get; private set; }

    public Book() { }

    private Book(int id, string title, string authorFullName, string genre, string? coverImageUrl)
    {
        Id = id;
        Genre = genre;
        Title = title;
        CoverImageUrl = coverImageUrl;
        AuthorFullName = authorFullName;
    }

    public static (Book book, ICollection<string> errors) Create(
        int id,
        string title,
        string authorFullName,
        string genre,
        string? coverImageUrl
    )
    {
        ICollection<string> errors = new List<string>();

        if (string.IsNullOrWhiteSpace(title))
        {
            errors.Add("Title Is Empty");

            return (new Book(), errors);
        }

        if (string.IsNullOrWhiteSpace(authorFullName))
        {
            errors.Add("Author's Fullname Is Empty");

            return (new Book(), errors);
        }

        if (string.IsNullOrWhiteSpace(genre))
        {
            errors.Add("Writer's Name Is Empty");

            return (new Book(), errors);
        }

        return (new Book(id, title, authorFullName, genre, coverImageUrl), errors);
    }
}
