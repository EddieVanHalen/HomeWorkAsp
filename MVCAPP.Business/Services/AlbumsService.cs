using MVCAPP.Domain.Models.Abstractions.Albums;
using MVCAPP.Domain.Models.Entities;
using MVCAPP.Models;
using MVCAPP.Models.Abstractions;

namespace MVCAPP.Business.Services;

public class AlbumsService : IAlbumsService
{
    private readonly IAlbumsRepository _albumsRepository;

    public AlbumsService(IAlbumsRepository albumsRepository)
    {
        _albumsRepository = albumsRepository;
    }

    public async Task<List<Album>> GetAllAsync()
    {
        return await _albumsRepository.GetAllAsync();
    }

    public async Task<Album> GetByIdAsync(int id)
    {
        return await _albumsRepository.GetByIdAsync(id);
    }

    public async Task<int> AddAsync(int artistId, string title, string imageUrl)
    {
        return await _albumsRepository.AddAsync(artistId, title, imageUrl);
    }

    public async Task<int> UpdateAsync(int id, int artistId, string title, string imageUrl)
    {
        return await _albumsRepository.UpdateAsync(id, artistId, title, imageUrl);
    }

    public async Task<int> DeleteAsync(int id)
    {
        return await _albumsRepository.DeleteAsync(id);
    }
}