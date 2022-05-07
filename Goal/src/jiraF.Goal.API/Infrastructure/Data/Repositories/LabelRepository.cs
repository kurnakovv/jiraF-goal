using jiraF.Goal.API.Contracts;
using jiraF.Goal.API.Domain;
using jiraF.Goal.API.Infrastructure.Data.Contexts;
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
            .Select(x => new LabelModel(new Title(x.Title)))
            .ToListAsync();
    }

    public async Task<LabelModel> GetByIdAsync(Guid id)
    {
        return await _dbContext.Labels
            .Where(x => x.Id == id)
            .Select(x => new LabelModel(new Title(x.Title)))
            .FirstOrDefaultAsync();
    }
}
