namespace MVCAPP.DataAccess.Entities;

public class AlbumEntity
{
    public int Id { get; set; }

    public int ArtistId { get; set; }

    public string Title { get; set; } = String.Empty;

    public string ImageUrl { get; set; } = null!;

    public virtual ArtistEntity Artist { get; set; } = null!;
    public virtual ICollection<SongEntity> Songs { get; set; } = null!;
}