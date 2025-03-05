using MVCAPP.Domain.Models.Entities;

namespace MVCAPP.DTOs;

public class ArtistDTO
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public string ImageUrl { get; set; } = null!;
    public Album[] Albums { get; set; } = [];
}