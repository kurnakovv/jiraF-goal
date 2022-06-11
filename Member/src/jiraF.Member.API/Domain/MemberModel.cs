namespace jiraF.Member.API.Domain;

public class MemberModel
{
    public Guid Number { get; }
    public DateTime DateOfCreate { get; }
    public string Name { get; }

    public MemberModel(
        string name)
    {
        Name = name;
    }

    public MemberModel(
        Guid number,
        DateTime dateOfCreate,
        string name)
    {
        Number = number;
        DateOfCreate = dateOfCreate;
        Name = name;
    }
}
