using MVCAPP.Domain.Models.Abstractions.Books;
using MVCAPP.Domain.Models.Entities;

namespace MVCAPP.Business.Services;

public class BooksService : IBooksService
{
    private readonly IBooksRepository _repository;

    public BooksService(IBooksRepository repository)
    {
        _repository = repository;
    }

    public async Task<List<Book>> GetAllAsync()
    {
        return await _repository.GetAllAsync();
    }

    public async Task<Book> GetByIdAsync(int id)
    {
        return await _repository.GetByIdAsync(id);
    }

    public async Task<int> AddAsync(string title, string authorFullName, string genres, string? coverImageUrl)
    {
        return await _repository.AddAsync(title, authorFullName, genres, coverImageUrl);
    }

    public async Task<int> UpdateAsync(int id, string title, string authorFullName, string genre, string? coverImageUrl)
    {
        return await _repository.UpdateAsync(id, title, authorFullName, genre, coverImageUrl);
    }

    public async Task<int> DeleteAsync(int id)
    {
        return await _repository.DeleteAsync(id);
    }
}