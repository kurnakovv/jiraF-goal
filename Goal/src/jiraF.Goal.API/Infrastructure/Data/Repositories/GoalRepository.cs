using jiraF.Goal.API.Contracts;
using jiraF.Goal.API.Domain;
using jiraF.Goal.API.Domain.Dtos;
using jiraF.Goal.API.Infrastructure.Data.Contexts;
using jiraF.Goal.API.ValueObjects;
using Microsoft.EntityFrameworkCore;

namespace jiraF.Goal.API.Infrastructure.Data.Repositories;

public class GoalRepository : IGoalRepository
{
    private readonly AppDbContext _dbContext;

    public GoalRepository(
        AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<IEnumerable<GoalModel>> GetAsync()
    {
        return await _dbContext.Goals
            .Select(x => new GoalModel(
                new Title(x.Title),
                new Description(x.Description),
                new User(),
                new User(),
                new LabelModel(new Title(x.LabelId.ToString()))))
            .ToListAsync();
    }
}
