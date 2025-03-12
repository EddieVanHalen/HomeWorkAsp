using MVCAPP.Domain.Models.Entities;

namespace MVCAPP.Domain.Models.Abstractions.Writers;

public interface IWritersRepository
{
    Task<List<Writer>> GetAllAsync();
    Task<Writer> GetByIdAsync(int id);
    Task<int> AddAsync(string name, string surname, string? imageUrl);
    Task<int> UpdateAsync(int id, string name, string surname, string? imageUrl);
    Task<int> DeleteAsync(int id);
}