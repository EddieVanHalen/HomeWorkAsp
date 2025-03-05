namespace MVCAPP.DataAccess.Entities;

public class SongEntity
{
    public int Id { get; set; }
       
    public int AlbumId { get; set; }
        
    public string Title { get; set; } = String.Empty;

    public virtual AlbumEntity Album { get; set; } = null!;
}