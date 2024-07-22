using jiraF.Goal.API.GlobalVariables;
using jiraF.Goal.API.Infrastructure.Data.Contexts;
using jiraF.Goal.API.Infrastructure.Data.Entities;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Emit;

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
            var goal1 = _dbContext.Goals.FirstOrDefault(x => x.Id == new Guid("a27723d9-fd4c-4b83-add8-f1c9152585ea"));
            if (goal1 == null)
            {
                _dbContext.Goals.Add(new GoalEntity() { Id = new Guid("a27723d9-fd4c-4b83-add8-f1c9152585ea"), AssigneeId = new Guid(DefaultMemberVariables.Id), ReporterId = new Guid(DefaultMemberVariables.Id), LabelId = _labelId, DateOfCreate = DateTime.UtcNow, Description = "Test desc1", Title = "Test title1" });
            }
            var goal2 = _dbContext.Goals.FirstOrDefault(x => x.Id == new Guid("a27723d9-fd4c-4b83-add8-f1c9152585eb"));
            if (goal2 == null)
            {
                _dbContext.Goals.Add(new GoalEntity() { Id = new Guid("a27723d9-fd4c-4b83-add8-f1c9152585eb"), AssigneeId = new Guid(DefaultMemberVariables.Id), ReporterId = new Guid(DefaultMemberVariables.Id), LabelId = _labelId, DateOfCreate = DateTime.UtcNow, Description = "Test desc2", Title = "Test title2" });
            }
            var goal3 = _dbContext.Goals.FirstOrDefault(x => x.Id == new Guid("a27723d9-fd4c-4b83-add8-f1c9152585ec"));
            if (goal3 == null)
            {
                _dbContext.Goals.Add(new GoalEntity() { Id = new Guid("a27723d9-fd4c-4b83-add8-f1c9152585ec"), AssigneeId = new Guid(DefaultMemberVariables.Id), ReporterId = new Guid(DefaultMemberVariables.Id), LabelId = _labelId, DateOfCreate = DateTime.UtcNow, Description = "Test desc3", Title = "Test title3" });
            }
            
            var label1 = _dbContext.Labels.FirstOrDefault(x => x.Id == new Guid("4674f93c-6331-4e63-b298-349619fa8741"));
            if (label1 == null)
            {
                _dbContext.Labels.Add(new LabelEntity() { Id = new Guid("4674f93c-6331-4e63-b298-349619fa8741"), Title = "Test title1" });
            }
            var label2 = _dbContext.Labels.FirstOrDefault(x => x.Id == new Guid("4674f93c-6331-4e63-b298-349619fa8742"));
            if (label2 == null)
            {
                _dbContext.Labels.Add(new LabelEntity() { Id = new Guid("4674f93c-6331-4e63-b298-349619fa8742"), Title = "Test title2" });
            }
            var label3 = _dbContext.Labels.FirstOrDefault(x => x.Id == new Guid("4674f93c-6331-4e63-b298-349619fa8743"));
            if (label3 == null)
            {
                _dbContext.Labels.Add(new LabelEntity() { Id = new Guid("4674f93c-6331-4e63-b298-349619fa8743"), Title = "Test title3" });
            }
            _dbContext.SaveChanges();
        }
    }
}
