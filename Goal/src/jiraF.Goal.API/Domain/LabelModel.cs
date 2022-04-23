using jiraF.Goal.API.ValueObjects;

namespace jiraF.Goal.API.Domain;

public class LabelModel
{
    public Title Title { get; set; }

    public LabelModel(
        Title title)
    {
        Title = title;
    }
}
