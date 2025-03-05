using MVCAPP.Domain.Models.Entities;

namespace MVCAPP.Models.Abstractions;

public interface IArtistsService
{
    Task<List<Artist>> GetAllAsync();
    Task<Artist> GetByIdAsync(int id);
    Task<int> AddAsync(int bandId, string name, string imageUrl);
    Task<int> UpdateAsync(int id, int bandId, string name, string imageUrl);
    Task<int> DeleteAsync(int id);
}