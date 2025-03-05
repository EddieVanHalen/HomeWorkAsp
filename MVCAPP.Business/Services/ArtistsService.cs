using MVCAPP.Domain.Models.Entities;
using MVCAPP.Models;
using MVCAPP.Models.Abstractions;

namespace MVCAPP.Business.Services;

public class ArtistsService : IArtistsService
{
    private readonly IArtistsRepository _artistsRepository;

    public ArtistsService(IArtistsRepository artistsRepository)
    {
        _artistsRepository = artistsRepository;
    }

    public async Task<List<Artist>> GetAllAsync()
    {
        return await _artistsRepository.GetAllAsync();
    }

    public async Task<Artist> GetByIdAsync(int id)
    {
        return await _artistsRepository.GetByIdAsync(id);
    }

    public async Task<int> AddAsync(int bandId, string name, string imageUrl)
    {
        return await _artistsRepository.AddAsync(bandId, name, imageUrl);
    }

    public async Task<int> UpdateAsync(int id, int bandId, string name, string imageUrl)
    {
        return await _artistsRepository.UpdateAsync(id, bandId, name, imageUrl);
    }

    public async Task<int> DeleteAsync(int id)
    {
        return await _artistsRepository.DeleteAsync(id);
    }
}