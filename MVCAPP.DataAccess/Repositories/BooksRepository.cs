using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using MVCAPP.DataAccess.Entities;
using MVCAPP.Domain.Models.Abstractions.Books;
using MVCAPP.Domain.Models.Entities;

namespace MVCAPP.DataAccess.Repositories;

public class BooksRepository : IBooksRepository
{
    private readonly ApplicationDbContext _dbContext;
    private readonly ILogger<BooksRepository> _logger;

    public BooksRepository(ApplicationDbContext dbContext, ILogger<BooksRepository> logger)
    {
        _dbContext = dbContext;
        _logger = logger;
    }

    public async Task<List<Book>> GetAllAsync()
    {
        try
        {
            List<BookEntity> bookEntities = await _dbContext.Books.AsNoTracking().ToListAsync();

            return bookEntities
                .Select(b =>
                    Book.Create(b.Id, b.Title, b.AuthorFullName, b.Genre, b.CoverImageUrl).book
                )
                .ToList();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Error occurred while fetching all books : {ex.Message}");
            return new List<Book>();
        }
    }

    public async Task<Book> GetByIdAsync(int id)
    {
        try
        {
            BookEntity? bookEntity = await _dbContext.Books.FirstOrDefaultAsync(a => a.Id == id);

            if (bookEntity is null)
            {
                return new Book();
            }

            return Book.Create(
                bookEntity.Id,
                bookEntity.Title,
                bookEntity.AuthorFullName,
                bookEntity.Genre,
                bookEntity.CoverImageUrl
            ).book;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Error occurred while getting book by id : {ex.Message}");
            return new Book();
        }
    }

    public async Task<int> AddAsync(
        string title,
        string authorFullname,
        string genres,
        string? coverImageUrl
    )
    {
        try
        {
            BookEntity bookEntity = new BookEntity
            {
                Title = title,
                Genre = genres,
                CoverImageUrl = coverImageUrl,
                AuthorFullName = authorFullname,
            };

            await _dbContext.Books.AddAsync(bookEntity);
            await _dbContext.SaveChangesAsync();

            return bookEntity.Id;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Error occurred while adding book : {ex.Message}");
            return -1;
        }
    }

    public async Task<int> UpdateAsync(
        int id,
        string title,
        string authorFullname,
        string genre,
        string? coverImageUrl
    )
    {
        try
        {
            await _dbContext
                .Books.Where(b => b.Id == id)
                .ExecuteUpdateAsync(setters =>
                    setters
                        .SetProperty(x => x.Title, title)
                        .SetProperty(x => x.Genre, genre)
                        .SetProperty(x => x.CoverImageUrl, coverImageUrl)
                        .SetProperty(x => x.AuthorFullName, authorFullname)
                );

            await _dbContext.SaveChangesAsync();

            return id;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Error occurred while updating book : {ex.Message}");
            return -1;
        }
    }

    public async Task<int> DeleteAsync(int id)
    {
        try
        {
            await _dbContext.Books.Where(x => x.Id == id).ExecuteDeleteAsync();

            await _dbContext.SaveChangesAsync();

            return id;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Error occurred while deleting book : {ex.Message}");
            return -1;
        }
    }
}
