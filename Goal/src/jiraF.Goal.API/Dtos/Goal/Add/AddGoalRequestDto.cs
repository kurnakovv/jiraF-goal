using jiraF.Goal.API.Dtos.Label;

namespace jiraF.Goal.API.Dtos.Goal.Add;

public class AddGoalRequestDto
{
    public string Title { get; set; }
    public string Description { get; set; }
    public Guid? ReporterId { get; set; }
    public Guid? AssigneeId { get; set; }
    public string LabelTitle { get; set; }
}
