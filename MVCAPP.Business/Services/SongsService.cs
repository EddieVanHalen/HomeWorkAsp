using MVCAPP.Domain.Models.Entities;
using MVCAPP.Models;
using MVCAPP.Models.Abstractions;

namespace MVCAPP.Business.Services;

public class SongsService : ISongsService
{
    private readonly ISongsRepository _songsRepository;

    public SongsService(ISongsRepository songsRepository)
    {
        _songsRepository = songsRepository;
    }

    public async Task<List<Song>> GetAllAsync()
    {
        return await _songsRepository.GetAllAsync();
    }

    public async Task<Song> GetByIdAsync(int id)
    {
        return await _songsRepository.GetByIdAsync(id);
    }

    public async Task<int> AddAsync(int albumId, string title)
    {
        return await _songsRepository.AddAsync(albumId, title);
    }

    public async Task<int> UpdateAsync(int id, int albumId, string title)
    {
        return await _songsRepository.UpdateAsync(id, albumId, title);
    }

    public async Task<int> DeleteAsync(int id)
    {
        return await _songsRepository.DeleteAsync(id);
    }
}