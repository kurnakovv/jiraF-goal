using jiraF.Goal.API.GlobalVariables;
using jiraF.Goal.API.Infrastructure.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace jiraF.Goal.API.Infrastructure.Data.Contexts;

public class AppDbContext : DbContext
{
    private readonly static Guid _labelId = new("4674f93c-6331-4e63-b298-349619fa8741");
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) 
    {
#if DEBUG
        var goal1 = this.Goals.FirstOrDefault(x => x.Id == new Guid("a27723d9-fd4c-4b83-add8-f1c9152585ea"));
        if (goal1 == null)
        {
            this.Goals.Add(new GoalEntity() { Id = new Guid("a27723d9-fd4c-4b83-add8-f1c9152585ea"), AssigneeId = new Guid(DefaultMemberVariables.Id), ReporterId = new Guid(DefaultMemberVariables.Id), LabelId = _labelId, DateOfCreate = DateTime.UtcNow, Description = "Test desc1", Title = "Test title1" });
        }
        var goal2 = this.Goals.FirstOrDefault(x => x.Id == new Guid("a27723d9-fd4c-4b83-add8-f1c9152585eb"));
        if (goal2 == null)
        {
            this.Goals.Add(new GoalEntity() { Id = new Guid("a27723d9-fd4c-4b83-add8-f1c9152585eb"), AssigneeId = new Guid(DefaultMemberVariables.Id), ReporterId = new Guid(DefaultMemberVariables.Id), LabelId = _labelId, DateOfCreate = DateTime.UtcNow, Description = "Test desc2", Title = "Test title2" });
        }
        var goal3 = this.Goals.FirstOrDefault(x => x.Id == new Guid("a27723d9-fd4c-4b83-add8-f1c9152585ec"));
        if (goal3 == null)
        {
            this.Goals.Add(new GoalEntity() { Id = new Guid("a27723d9-fd4c-4b83-add8-f1c9152585ec"), AssigneeId = new Guid(DefaultMemberVariables.Id), ReporterId = new Guid(DefaultMemberVariables.Id), LabelId = _labelId, DateOfCreate = DateTime.UtcNow, Description = "Test desc3", Title = "Test title3" });
        }

        var label1 = this.Labels.FirstOrDefault(x => x.Id == new Guid("4674f93c-6331-4e63-b298-349619fa8741"));
        if (label1 == null)
        {
            this.Labels.Add(new LabelEntity() { Id = new Guid("4674f93c-6331-4e63-b298-349619fa8741"), Title = "Test title1" });
        }
        var label2 = this.Labels.FirstOrDefault(x => x.Id == new Guid("4674f93c-6331-4e63-b298-349619fa8742"));
        if (label2 == null)
        {
            this.Labels.Add(new LabelEntity() { Id = new Guid("4674f93c-6331-4e63-b298-349619fa8742"), Title = "Test title2" });
        }
        var label3 = this.Labels.FirstOrDefault(x => x.Id == new Guid("4674f93c-6331-4e63-b298-349619fa8743"));
        if (label3 == null)
        {
            this.Labels.Add(new LabelEntity() { Id = new Guid("4674f93c-6331-4e63-b298-349619fa8743"), Title = "Test title3" });
        }
        this.SaveChanges();
#endif
    }

    public DbSet<GoalEntity> Goals { get; set; }
    public DbSet<LabelEntity> Labels { get; set; }
}
