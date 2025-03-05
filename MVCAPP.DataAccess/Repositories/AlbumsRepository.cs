using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using MVCAPP.DataAccess.Entities;
using MVCAPP.Domain.Models.Abstractions.Albums;
using MVCAPP.Domain.Models.Entities;
using MVCAPP.Models;
using MVCAPP.Models.Abstractions;

namespace MVCAPP.DataAccess.Repositories;

public class AlbumsRepository : IAlbumsRepository
{
    private readonly ApplicationDbContext _dbContext;

    private readonly ILogger<AlbumsRepository> _logger;

    public AlbumsRepository(ApplicationDbContext dbContext, ILogger<AlbumsRepository> logger)
    {
        _dbContext = dbContext;
        _logger = logger;
    }

    public async Task<List<Album>> GetAllAsync()
    {
        try
        {
            ICollection<AlbumEntity> albumEntities = await _dbContext.Albums.AsNoTracking().ToListAsync();

            _logger.LogInformation($"Retrieved {albumEntities.Count} albums");
            return albumEntities.Select(a => Album.Create(a.Id, a.ArtistId, a.Title, a.ImageUrl).album).ToList();
        }
        catch (Exception e)
        {
            _logger.LogError(e, e.Message);
            return new List<Album>();
        }
    }

    public async Task<Album> GetByIdAsync(int id)
    {
        try
        {
            AlbumEntity? albumEntity = await _dbContext.Albums.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);

            if (albumEntity is null)
            {
                return new Album();
            }

            _logger.LogInformation($"Retrieved {albumEntity.Id} album");
            return Album.Create(albumEntity.Id, albumEntity.ArtistId, albumEntity.Title, albumEntity.ImageUrl).album;
        }
        catch (Exception e)
        {
            _logger.LogError(e, e.Message);
            return new Album();
        }
    }

    public async Task<int> AddAsync(int artistId, string title, string imageUrl)
    {
        try
        {
            AlbumEntity albumEntity = new AlbumEntity
            {
                Id = 0, 
                ArtistId = artistId,
                Title = title,
                ImageUrl = imageUrl
            };

            await _dbContext.Albums.AddAsync(albumEntity);

            await _dbContext.SaveChangesAsync();

            _logger.LogInformation($"Added {albumEntity.Id} album");
            return albumEntity.Id;
        }
        catch (Exception e)
        {
            _logger.LogError(e, e.Message);
            return -1;
        }
    }

    public async Task<int> UpdateAsync(int id, int artistId, string title, string imageUrl)
    {
        try
        {
            await _dbContext.Albums
                .Where(x => x.Id == id)
                .ExecuteUpdateAsync(setters => setters
                    .SetProperty(x => x.ArtistId, artistId)
                    .SetProperty(x => x.Title, title)
                    .SetProperty(x => x.ImageUrl, imageUrl));
            
            await _dbContext.SaveChangesAsync();
            
            _logger.LogInformation($"Updated {id} album");
            
            return id;
        }
        catch (Exception e)
        {
            _logger.LogError(e, e.Message);
            return -1;
        }
    }

    public async Task<int> DeleteAsync(int id)
    {
        try
        {
            await _dbContext.Albums.Where(x => x.Id == id).ExecuteDeleteAsync();
            await _dbContext.SaveChangesAsync();
            
            _logger.LogInformation($"Deleted {id} album");
            return id;
        }
        catch (Exception e)
        {
            _logger.LogError(e, e.Message);
            return -1;
        }
    }
}