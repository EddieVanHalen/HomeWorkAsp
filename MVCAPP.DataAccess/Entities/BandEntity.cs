namespace MVCAPP.DataAccess.Entities;

public class BandEntity
{
	public int Id { get; set; }
                                                         
	public string Title { get; set; } = string.Empty;
	
	public virtual ICollection<ArtistEntity> Artists { get; set; } = null!;
}