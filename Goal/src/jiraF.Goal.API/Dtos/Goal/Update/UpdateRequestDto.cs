using jiraF.Goal.API.Dtos.Label;

namespace jiraF.Goal.API.Dtos.Goal.Update;

public class UpdateRequestDto
{
    public Guid Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public Guid ReporterId { get; set; }
    public Guid AssigneeId { get; set; }
    public LabelDto Label { get; set; }
}
