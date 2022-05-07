using jiraF.Goal.API.Domain;

namespace jiraF.Goal.API.Contracts;

public interface ILabelRepository
{
    Task<IEnumerable<LabelModel>> GetAsync();
    Task<LabelModel> GetByIdAsync(Guid id);
    Task AddAsync(LabelModel model);
    Task UpdateAsync(Guid id, LabelModel model);
}
