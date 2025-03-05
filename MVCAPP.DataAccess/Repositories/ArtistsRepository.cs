using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using MVCAPP.DataAccess.Entities;
using MVCAPP.Domain.Models.Entities;
using MVCAPP.Models.Abstractions;

namespace MVCAPP.DataAccess.Repositories;

public class ArtistsRepository : IArtistsRepository
{
    private readonly ApplicationDbContext _dbContext;

    private readonly ILogger<ArtistsRepository> _logger;

    public ArtistsRepository(ApplicationDbContext dbContext, ILogger<ArtistsRepository> logger)
    {
        _dbContext = dbContext;
        _logger = logger;
    }

    public async Task<List<Artist>> GetAllAsync()
    {
        try
        {
            ICollection<ArtistEntity> artistsEntity = await _dbContext.Artists.AsNoTracking().ToListAsync();

            _logger.LogInformation($"Retrieved {artistsEntity.Count} artists");
            return artistsEntity.Select(a => Artist.Create(a.Id, a.BandId, a.Name, a.ImageUrl).artist).ToList();
        }
        catch (Exception e)
        {
            _logger.LogError(e, e.Message);
            return new List<Artist>();
        }
    }

    public async Task<Artist> GetByIdAsync(int id)
    {
        try
        {
            ArtistEntity? artistEntity = await _dbContext.Artists.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);

            if (artistEntity is null)
            {
                return new Artist();
            }

            _logger.LogInformation($"Retrieved artist with id {artistEntity.Id}");
            return Artist.Create(artistEntity.Id, artistEntity.BandId, artistEntity.Name, artistEntity.ImageUrl).artist;
        }
        catch (Exception e)
        {
            _logger.LogError(e, e.Message);
            return new Artist();
        }
    }

    public async Task<int> AddAsync(int bandId, string name, string imageUrl)
    {
        try
        {
            ArtistEntity artistEntity = new ArtistEntity
            {
                Id = 0,
                BandId = bandId,
                Name = name,
                ImageUrl = imageUrl
            };

            await _dbContext.Artists.AddAsync(new ArtistEntity
            {
                Id = 0,
                BandId = bandId,
                Name = name,
                ImageUrl = imageUrl
            });

            await _dbContext.SaveChangesAsync();

            _logger.LogInformation($"Artist with id {artistEntity.Id} added");

            return artistEntity.Id;
        }
        catch (Exception e)
        {
            _logger.LogError(e, e.Message);
            return -1;
        }
    }

    public async Task<int> UpdateAsync(int id, int? bandId, string name, string imageUrl)
    {
        try
        {
            await _dbContext.Artists.Where(x => x.Id == id).ExecuteUpdateAsync(setters => setters
                .SetProperty(x => x.Name, name)
                .SetProperty(x => x.BandId, bandId)
                .SetProperty(x => x.ImageUrl, imageUrl));
            
            await _dbContext.SaveChangesAsync();
            
            _logger.LogInformation($"Artist with id {id} updated");
            
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
            await _dbContext.Artists.Where(x => x.Id == id).ExecuteDeleteAsync();
            await _dbContext.SaveChangesAsync();
            
            _logger.LogInformation($"Artist with id {id} deleted");
            
            return id;
        }
        catch (Exception e)
        {
            _logger.LogError(e, e.Message);
            return -1;
        }
    }
}