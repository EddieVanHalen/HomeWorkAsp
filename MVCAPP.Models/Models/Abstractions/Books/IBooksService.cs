using MVCAPP.Domain.Models.Entities;

namespace MVCAPP.Domain.Models.Abstractions.Books;

public interface IBooksService
{
    Task<List<Book>> GetAllAsync();
    Task<Book> GetByIdAsync(int id);
    Task<int> AddAsync(string title, string authorFullname, string genres, string? coverImageUrl);
    Task<int> UpdateAsync(int id, string title, string authorFullname, string genre, string? coverImageUrl);
    Task<int> DeleteAsync(int id);
}