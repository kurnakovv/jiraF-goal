namespace jiraF.Member.API.Domain;

public class MemberModel
{
    public Guid Number { get; }
    public DateTime DateOfRegistration { get; }
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
        DateOfRegistration = dateOfCreate;
        Name = name;
    }
}
