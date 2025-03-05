using Microsoft.AspNetCore.Mvc;
using MVCAPP.Domain.Models.Abstractions.Albums;
using MVCAPP.Domain.Models.Entities;
using MVCAPP.DTOs;
using MVCAPP.Models;
using MVCAPP.Models.Abstractions;

namespace MVCAPP.Controllers;

public class SongsController : Controller
{
    private readonly ISongsService _songsService;
    private readonly IAlbumsService _albumsService;
    private readonly IArtistsService _artistsService;

    public SongsController(ISongsService songsService, IAlbumsService albumsService, IArtistsService artistsService)
    {
        _songsService = songsService;
        _albumsService = albumsService;
        _artistsService = artistsService;
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

    [HttpGet]
    public async Task<IActionResult> Details(int id)
    {
        Song song = await _songsService.GetByIdAsync(id);

        Album album = await _albumsService.GetByIdAsync(song.AlbumId);

        Artist artist = await _artistsService.GetByIdAsync(album.ArtistId);

        SongDTO dto = new SongDTO
        {
            Id = song.Id,
            Title = song.Title,
            ArtistName = artist.Name,
            ImageUrl = album.ImageUrl,
            ArtistId = artist.Id,
        };

        return View(dto);
    }
}