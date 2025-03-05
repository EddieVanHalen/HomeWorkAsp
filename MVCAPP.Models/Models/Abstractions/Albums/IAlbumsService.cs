using MVCAPP.Domain.Models.Entities;

namespace MVCAPP.Domain.Models.Abstractions.Albums;

public interface IAlbumsService
{
    Task<List<Album>> GetAllAsync();
    Task<Album> GetByIdAsync(int id);
    Task<int> AddAsync(int artistId, string title, string imageUrl);
    Task<int> UpdateAsync(int id, int artistId, string title, string imageUrl);
    Task<int> DeleteAsync(int id);
}