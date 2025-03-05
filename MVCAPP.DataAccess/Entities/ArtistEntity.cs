namespace MVCAPP.DataAccess.Entities;

public class ArtistEntity
{
    public int Id { get; set; }

    public int? BandId { get; set; }

    public string Name { get; set; } = string.Empty;

    public string ImageUrl { get; set; } = null!;
    
    public virtual BandEntity Band { get; set; } = null;
    public virtual ICollection<AlbumEntity> Albums { get; set; } = null;
}