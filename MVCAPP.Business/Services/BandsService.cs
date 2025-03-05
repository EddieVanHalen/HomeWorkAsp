using MVCAPP.Domain.Models.Entities;
using MVCAPP.Models;
using MVCAPP.Models.Abstractions;

namespace MVCAPP.Business.Services;

public class BandsService : IBandsService
{
    private readonly IBandsRepository _bandsRepository;

    public BandsService(IBandsRepository bandsRepository)
    {
        _bandsRepository = bandsRepository;
    }

    public async Task<ICollection<Band>> GetAllAsync()
    {
        return await _bandsRepository.GetAllAsync();
    }

    public async Task<Band> GetByIdAsync(int id)
    {
        return await _bandsRepository.GetByIdAsync(id);
    }

    public async Task<int> AddAsync(string title)
    {
        return await _bandsRepository.AddAsync(title);
    }

    public async Task<int> UpdateAsync(int id, string title)
    {
        return await _bandsRepository.UpdateAsync(id, title);
    }

    public async Task<int> DeleteAsync(int id)
    {
        return await _bandsRepository.DeleteAsync(id);
    }
}