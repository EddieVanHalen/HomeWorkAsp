using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MVCAPP.Domain.Models.Abstractions.Albums;
using MVCAPP.Domain.Models.Entities;
using MVCAPP.Models.Abstractions;

namespace MVCAPP.Areas.Admin.Controllers;

[Area("Admin")]
[Authorize(Roles = "Admin")]
public class SongsController : Controller
{
    private readonly ISongsService _songsService;
    private readonly IAlbumsService _albumsService;

    public SongsController(ISongsService songsService, IAlbumsService albumsService)
    {
        _songsService = songsService;
        _albumsService = albumsService;
    }
    
    [HttpGet]
    public async Task<IActionResult> Index()
    {
        Dictionary<Song, Album> songsAndAlbums = [];

        List<Song> songs = await _songsService.GetAllAsync();

        foreach (var song in songs)
        {
            Album temp = await _albumsService.GetByIdAsync(song.AlbumId);

            songsAndAlbums.Add(song, temp);
        }

        return View(songsAndAlbums);
    }
}