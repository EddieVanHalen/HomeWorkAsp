using Microsoft.AspNetCore.Mvc;
using MVCAPP.DataAccess;
using MVCAPP.Domain.Models.Abstractions.Albums;
using MVCAPP.Domain.Models.Entities;
using MVCAPP.DTOs;
using MVCAPP.Models.Abstractions;

namespace MVCAPP.Controllers;

public class ArtistsController : Controller
{
    private readonly IArtistsService _artistsService;
    private readonly IAlbumsService _albumsService;

    public ArtistsController(IArtistsService artistsService, IAlbumsService albumsService)
    {
        _artistsService = artistsService;
        _albumsService = albumsService;
    }

    // GET
    public async Task<IActionResult> Index()
    {
        return View();
    }

    public async Task<IActionResult> Details(int id)
    {
        Artist artist = await _artistsService.GetByIdAsync(id);

        List<Album> albums = await _albumsService.GetAllAsync();

        Album[] artistAlbums = albums.Where(a => a.ArtistId == id).Take(3).ToArray();
        
        ArtistDTO dto = new ArtistDTO
        {
            Id = artist.Id,
            Name = artist.Name,
            ImageUrl = artist.ImageUrl,
            Albums = artistAlbums
        };
        
        return View(dto);
    }
}