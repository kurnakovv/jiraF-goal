using jiraF.Goal.API.Contracts;
using jiraF.Goal.API.Domain;
using jiraF.Goal.API.Infrastructure.Data.Contexts;
using jiraF.Goal.API.Infrastructure.Data.Entities;
using jiraF.Goal.API.ValueObjects;
using Microsoft.EntityFrameworkCore;

namespace jiraF.Goal.API.Infrastructure.Data.Repositories;

public class LabelRepository : ILabelRepository
{
    private readonly AppDbContext _dbContext;

    public LabelRepository(
        AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<IEnumerable<LabelModel>> GetAsync()
    {
        return await _dbContext.Labels
            .Select(x => new LabelModel(
                x.Id,
                new Title(x.Title)))
            .ToListAsync();
    }

    public async Task<LabelModel> GetByIdAsync(Guid id)
    {
        return await _dbContext.Labels
            .Where(x => x.Id == id)
            .Select(x => new LabelModel(
                x.Id,
                new Title(x.Title)))
            .FirstOrDefaultAsync();
    }

    public async Task<Guid> AddAsync(LabelModel model)
    {
        LabelEntity entity = new()
        {
            Title = model.Title.Value,
        };
        _dbContext.Labels.Add(entity);
        await _dbContext.SaveChangesAsync();
        return entity.Id;
    }

    public async Task UpdateAsync(Guid id, LabelModel model)
    {
        LabelEntity entity = await _dbContext.Labels.FirstOrDefaultAsync(x => x.Id == id);
        entity.Title = model.Title.Value;
        await _dbContext.SaveChangesAsync();
    }

    public async Task DeleteByIdAsync(Guid id)
    {
        LabelEntity entity = await _dbContext.Labels.FirstOrDefaultAsync(x => x.Id == id);
        _dbContext.Labels.Remove(entity);
        await _dbContext.SaveChangesAsync();
    }
}
