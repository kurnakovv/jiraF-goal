using jiraF.Goal.API.Domain.Dtos;
using jiraF.Goal.API.Exceptions;
using jiraF.Goal.API.ValueObjects;

namespace jiraF.Goal.API.Domain;

public class GoalModel
{
    public Guid Number { get; }
    public Title Title { get; }
    public Description Description { get;}
    public User Reporter { get; }
    public User Assignee { get; }
    public DateTime DateOfCreate { get; }
    public DateTime DateOfUpdate { get; }
    public LabelModel Label { get; private set; }

    public GoalModel(
        Title title,
        Description description,
        Guid reporterNumber,
        Guid assigneeNumber,
        string labelTitle)
    {
        Title = title;
        Description = description;
        Reporter = new User(reporterNumber);
        Assignee = new User(assigneeNumber);
        DateOfCreate = DateTime.UtcNow;
        DateOfUpdate = default(DateTime);
        Label = new LabelModel(new Title(labelTitle));
    }

    public GoalModel(
        Guid number,
        Title title,
        Description description,
        User reporter,
        User assignee,
        DateTime dateOfCreate,
        DateTime dateOfUpdate,
        LabelModel label)
    {
        Number = number;
        Title = title;
        Description = description;
        Reporter = reporter;
        Assignee = assignee;
        DateOfCreate = dateOfCreate;
        DateOfUpdate = dateOfUpdate;
        Label = label;
    }


    public void EditLabel(IEnumerable<LabelModel> availableLabels, string label)
    {
        bool containsLabel = availableLabels
            .Where(x => x.Title.Value == label).Any();

        if (!containsLabel)
            throw new DomainException($"Not contains label: '{label}'.");

        Label = new LabelModel(new Title(label));
    }
}
