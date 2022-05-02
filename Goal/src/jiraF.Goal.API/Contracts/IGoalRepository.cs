using jiraF.Goal.API.Domain;

namespace jiraF.Goal.API.Contracts;

public interface IGoalRepository
{
    Task<IEnumerable<GoalModel>> GetAsync();
    Task<GoalModel> GetByIdAsync(Guid id);
    Task AddAsync(GoalModel model);
    Task UpdateAsync(Guid id, GoalModel model);
}
