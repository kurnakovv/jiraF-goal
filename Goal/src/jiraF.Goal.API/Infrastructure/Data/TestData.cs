using jiraF.Goal.API.GlobalVariables;
using jiraF.Goal.API.Infrastructure.Data.Contexts;
using jiraF.Goal.API.Infrastructure.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace jiraF.Goal.API.Infrastructure.Data
{
    public class TestData
    {
        private readonly AppDbContext _dbContext;

        private readonly static Guid _labelId = new("4674f93c-6331-4e63-b298-349619fa8741");

        public TestData(
            AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void Seed()
        {
            _dbContext.ChangeTracker.Clear();
            if (!_dbContext.Goals.Any())
            {
                if (!_dbContext.Goals.Any(x => x.Id == new Guid("a27723d9-fd4c-4b83-add8-f1c9152585ea")))
                {
                    _dbContext.Goals.Add(new GoalEntity() { Id = new Guid("a27723d9-fd4c-4b83-add8-f1c9152585ea"), AssigneeId = new Guid(DefaultMemberVariables.Id), ReporterId = new Guid(DefaultMemberVariables.Id), LabelId = _labelId, DateOfCreate = DateTime.UtcNow, Description = "Test desc1", Title = "Test title1" });
                }
                if (!_dbContext.Goals.Any(x => x.Id == new Guid("a27723d9-fd4c-4b83-add8-f1c9152585eb")))
                {
                    _dbContext.Goals.Add(new GoalEntity() { Id = new Guid("a27723d9-fd4c-4b83-add8-f1c9152585eb"), AssigneeId = new Guid(DefaultMemberVariables.Id), ReporterId = new Guid(DefaultMemberVariables.Id), LabelId = _labelId, DateOfCreate = DateTime.UtcNow, Description = "Test desc2", Title = "Test title2" });
                }
                if (!_dbContext.Goals.Any(x => x.Id == new Guid("a27723d9-fd4c-4b83-add8-f1c9152585ec")))
                {
                    _dbContext.Goals.Add(new GoalEntity() { Id = new Guid("a27723d9-fd4c-4b83-add8-f1c9152585ec"), AssigneeId = new Guid(DefaultMemberVariables.Id), ReporterId = new Guid(DefaultMemberVariables.Id), LabelId = _labelId, DateOfCreate = DateTime.UtcNow, Description = "Test desc3", Title = "Test title3" });
                }
                _dbContext.SaveChanges();
                _dbContext.ChangeTracker.Clear();
            }

            if (!_dbContext.Labels.Any())
            {
                if (!_dbContext.Labels.Any(x => x.Id == new Guid("4674f93c-6331-4e63-b298-349619fa8741")))
                {
                    _dbContext.Labels.Add(new LabelEntity() { Id = new Guid("4674f93c-6331-4e63-b298-349619fa8741"), Title = "Test title1" });
                }
                if (!_dbContext.Labels.Any(x => x.Id == new Guid("4674f93c-6331-4e63-b298-349619fa8742")))
                {
                    _dbContext.Labels.Add(new LabelEntity() { Id = new Guid("4674f93c-6331-4e63-b298-349619fa8742"), Title = "Test title2" });
                }
                if (!_dbContext.Labels.Any(x => x.Id == new Guid("4674f93c-6331-4e63-b298-349619fa8743")))
                {
                    _dbContext.Labels.Add(new LabelEntity() { Id = new Guid("4674f93c-6331-4e63-b298-349619fa8743"), Title = "Test title3" });
                }
                _dbContext.SaveChanges();
                _dbContext.ChangeTracker.Clear();
            }
            _dbContext.ChangeTracker.Clear();
        }
    }
}
