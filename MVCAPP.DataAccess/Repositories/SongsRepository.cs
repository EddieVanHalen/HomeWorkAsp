using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using MVCAPP.DataAccess.Entities;
using MVCAPP.Domain.Models.Entities;
using MVCAPP.Models;
using MVCAPP.Models.Abstractions;

namespace MVCAPP.DataAccess.Repositories;

public class SongsRepository : ISongsRepository
{
    private readonly ApplicationDbContext _dbContext;

    private readonly ILogger<SongsRepository> _logger;

    public SongsRepository(ApplicationDbContext dbContext, ILogger<SongsRepository> logger)
    {
        _dbContext = dbContext;
        _logger = logger;
    }

    public async Task<List<Song>> GetAllAsync()
    {
        try
        {
            ICollection<SongEntity> songsEntities = await _dbContext.Songs.AsNoTracking().ToListAsync();

            _logger.LogInformation($"Retrieved {songsEntities.Count} songs");
            return songsEntities.Select(s => Song.Create(s.Id, s.AlbumId, s.Title).song).ToList();
        }
        catch (Exception e)
        {
            _logger.LogError(e, e.Message);
            return new List<Song>();
        }
    }

    public async Task<Song> GetByIdAsync(int id)
    {
        try
        {
            SongEntity? songEntity = await _dbContext.Songs.AsNoTracking().FirstOrDefaultAsync(s => s.Id == id);

            if (songEntity is null)
            {
                return new Song();
            }

            return Song.Create(songEntity.Id, songEntity.AlbumId, songEntity.Title).song;
        }
        catch (Exception e)
        {
            _logger.LogError(e, $"Failed to retrieve song with id {id}");
            return new Song();
        }
    }

    public async Task<int> AddAsync(int albumId, string title)
    {
        SongEntity songEntity = new SongEntity
        {
            Id = 0,
            AlbumId = albumId,
            Title = title
        };

        try
        {
            await _dbContext.Songs.AddAsync(songEntity);
            await _dbContext.SaveChangesAsync();
            _logger.LogInformation($"Added song with id {songEntity.Id} to database");
            return songEntity.Id;
        }
        catch (Exception e)
        {
            _logger.LogError(e, $"Failed to add song with id {songEntity.Id} to database");
            return -1;
        }
    }

    public async Task<int> UpdateAsync(int id, int albumId, string title)
    {
        try
        {
            await _dbContext.Songs
                .Where(a => a.Id == id)
                .ExecuteUpdateAsync(setters => setters
                    .SetProperty(x => x.Title, title));
            
            await _dbContext.SaveChangesAsync();
            
            _logger.LogInformation($"Updated song with id {id} to database");
            
            return id;
        }
        catch (Exception e)
        {
            _logger.LogError(e, $"Failed to update song with id {id} to database");
            return -1;
        }
    }

    public async Task<int> DeleteAsync(int id)
    {
        try
        {
            await _dbContext.Songs.Where(s => s.Id == id).ExecuteDeleteAsync();
        }
        catch (Exception e)
        {
            _logger.LogError(e, $"Failed to delete song with id {id}");
            return -1;
        }
        
        return 0;
    }
}