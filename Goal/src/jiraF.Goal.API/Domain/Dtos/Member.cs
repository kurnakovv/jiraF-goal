namespace jiraF.Goal.API.Domain.Dtos;

public class Member
{
    public Guid Number { get; }
    public string Name { get; }
    public string Img { get; }

    public Member() { }

    public Member(Guid number)
    {
        Number = number;
    }

    public Member(
        Guid number,
        string name,
        string img)
    {
        Number = number;
        Name = name;
        Img = img;
    }
}
