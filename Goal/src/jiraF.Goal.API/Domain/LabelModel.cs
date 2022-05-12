using jiraF.Goal.API.ValueObjects;

namespace jiraF.Goal.API.Domain;

public class LabelModel
{
    public Guid Number { get; }
    public Title Title { get; }

    public LabelModel(
        Title title)
    {
        Title = title;
    }

    public LabelModel(
        Guid number,
        Title title) : this(title)
    {
        Number = number;
    }
}
