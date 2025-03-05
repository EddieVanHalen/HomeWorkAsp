using MVCAPP.Domain.Models.Entities;

namespace MVCAPP.Models.Abstractions;

public interface IBandsRepository
{
    Task<ICollection<Band>> GetAllAsync();
    Task<Band> GetByIdAsync(int id);
    Task<int> AddAsync(string title);
    Task<int> UpdateAsync(int id, string title);
    Task<int> DeleteAsync(int id);
}