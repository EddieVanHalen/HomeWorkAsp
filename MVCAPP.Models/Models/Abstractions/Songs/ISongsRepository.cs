using MVCAPP.Domain.Models.Entities;

namespace MVCAPP.Models.Abstractions;

public interface ISongsRepository
{
    Task<List<Song>> GetAllAsync();
    Task<Song> GetByIdAsync(int id);
    Task<int> AddAsync(int albumId, string title);
    Task<int> UpdateAsync(int id, int albumId, string title);
    Task<int> DeleteAsync(int id);
}