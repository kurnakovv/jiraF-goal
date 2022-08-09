using jiraF.Goal.API.Contracts;
using jiraF.Goal.API.Domain;
using jiraF.Goal.API.Domain.Dtos;
using jiraF.Goal.API.Infrastructure.Data.Contexts;
using jiraF.Goal.API.Infrastructure.Data.Entities;
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
                x.Id,
                new Title(x.Title),
                new Description(x.Description),
                new Member(x.ReporterId),
                new Member(x.AssigneeId),
                x.DateOfCreate,
                x.DateOfUpdate,
                new LabelModel(
                    x.LabelId, 
                    new Title(_dbContext.Labels
                        .Where(l => l.Id == x.LabelId)
                        .Select(x => x.Title)
                        .FirstOrDefault() ?? x.LabelId.ToString()))))
            .ToListAsync();
    }

    public async Task<GoalModel> GetByIdAsync(Guid id)
    {
        return await _dbContext.Goals
            .Where(x => x.Id == id)
            .Select(x => new GoalModel(
                x.Id,
                new Title(x.Title),
                new Description(x.Description),
                new Member(),
                new Member(),
                x.DateOfCreate,
                x.DateOfUpdate,
                new LabelModel(
                    x.LabelId,
                    new Title(_dbContext.Labels
                        .Where(l => l.Id == x.LabelId)
                        .Select(x => x.Title)
                        .FirstOrDefault() ?? x.LabelId.ToString()))))
            .FirstOrDefaultAsync();
    }

    public async Task<Guid> AddAsync(GoalModel model)
    {
        Guid labelId = await GetLabelIdByTitle(model.Label.Title.Value);
        GoalEntity entity = new GoalEntity
        {
            Title = model.Title.Value,
            AssigneeId = model.Assignee.Number,
            ReporterId = model.Reporter.Number,
            LabelId = labelId,
            Description = model.Description.Value,
            DateOfCreate = model.DateOfCreate,
        };
        _dbContext.Goals.Add(entity);
        await _dbContext.SaveChangesAsync();
        return entity.Id;
    }

    public async Task UpdateAsync(Guid id, GoalModel model)
    {
        Guid labelId = await GetLabelIdByTitle(model.Label.Title.Value);
        GoalEntity entity = await _dbContext.Goals.FirstOrDefaultAsync(x => x.Id == id);
        entity.Title = model.Title.Value;
        entity.AssigneeId = model.Assignee.Number;
        entity.ReporterId = model.Reporter.Number;
        entity.DateOfUpdate = DateTime.UtcNow;
        entity.Description = model.Description.Value;
        entity.LabelId = labelId;
        await _dbContext.SaveChangesAsync();
    }

    public async Task DeleteByIdAsync(Guid id)
    {
        GoalEntity entity = await _dbContext.Goals.FirstOrDefaultAsync(x => x.Id == id);
        _dbContext.Goals.Remove(entity);
        await _dbContext.SaveChangesAsync();
    }


    private async Task<Guid> GetLabelIdByTitle(string title)
    {
        return await _dbContext.Labels
            .Where(x => x.Title == title)
            .Select(x => x.Id)
            .FirstOrDefaultAsync();
    }
}
