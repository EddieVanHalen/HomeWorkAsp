using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using MVCAPP.DataAccess.Entities;
using MVCAPP.Domain.Models.Entities;
using MVCAPP.Models;
using MVCAPP.Models.Abstractions;

namespace MVCAPP.DataAccess.Repositories;

public class BandsRepository : IBandsRepository
{
    private readonly ApplicationDbContext _dbContext;

    private readonly ILogger<BandsRepository> _logger;

    public BandsRepository(ApplicationDbContext dbContext, ILogger<BandsRepository> logger)
    {
        _dbContext = dbContext;
        _logger = logger;
    }

    public async Task<ICollection<Band>> GetAllAsync()
    {
        try
        {
            ICollection<BandEntity> bandEntities = await _dbContext.Bands.AsNoTracking().ToListAsync();

            _logger.LogInformation($"Retrieved {bandEntities.Count} Bands");
            return bandEntities.Select(x => Band.Create(x.Id, x.Title).Band).ToList();
        }
        catch (Exception e)
        {
            _logger.LogError(e, e.Message);
            return new List<Band>();
        }
    }

    public async Task<Band> GetByIdAsync(int id)
    {
        try
        {
            BandEntity? bandEntity = await _dbContext.Bands.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);

            if (bandEntity is null)
            {
                return new Band();
            }

            _logger.LogInformation($"Retrieved {bandEntity.Id} Band");
            return Band.Create(bandEntity.Id, bandEntity.Title).Band;
        }
        catch (Exception e)
        {
            _logger.LogError(e, e.Message);
            return new Band();
        }
    }

    public async Task<int> AddAsync(string title)
    {
        try
        {
            BandEntity bandEntity = new BandEntity
            {
                Id = 0,
                Title = title
            };

            await _dbContext.Bands.AddAsync(bandEntity);

            await _dbContext.SaveChangesAsync();

            _logger.LogInformation($"Added {bandEntity.Id} Band");
            return bandEntity.Id;
        }
        catch (Exception e)
        {
            _logger.LogError(e, e.Message);
            return -1;
        }
    }

    public async Task<int> UpdateAsync(int id, string title)
    {
        try
        {
            await _dbContext.Bands
                .Where(x => x.Id == id)
                .ExecuteUpdateAsync(setters => setters
                    .SetProperty(a => a.Title, title));
            
            await _dbContext.SaveChangesAsync();
            
            _logger.LogInformation($"Updated {id} Band");
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
            await _dbContext.Bands.Where(x => x.Id == id).ExecuteDeleteAsync();
            
            await _dbContext.SaveChangesAsync();
            
            _logger.LogInformation($"Deleted {id} Band");
            return id;
        }
        catch (Exception e)
        {
            _logger.LogError(e, e.Message);
            return -1;
        }
    }
}