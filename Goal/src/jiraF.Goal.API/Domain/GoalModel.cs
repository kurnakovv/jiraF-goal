using jiraF.Goal.API.Domain.Dtos;
using jiraF.Goal.API.Exceptions;
using jiraF.Goal.API.ValueObjects;

namespace jiraF.Goal.API.Domain;

public class GoalModel
{
    public Guid Number { get; }
    public Title Title { get; }
    public Description Description { get;}
    public Member Reporter { get; }
    public Member Assignee { get; }
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
        Reporter = new Member(reporterNumber);
        Assignee = new Member(assigneeNumber);
        DateOfCreate = DateTime.UtcNow;
        DateOfUpdate = default(DateTime);
        Label = new LabelModel(new Title(labelTitle));
    }

    public GoalModel(
        Guid number,
        Title title,
        Description description,
        Member reporter,
        Member assignee,
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
        if (!availableLabels.Any(x => x.Title.Value == label))
        {
            throw new DomainException($"Not contains label: '{label}'.");
        }
        Label = new LabelModel(new Title(label));
    }
}
