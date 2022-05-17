namespace jiraF.Goal.API.Domain.Dtos;

public class User
{
    public Guid Number { get; }
    public string Name { get; }
    public string Img { get; }

    public User() { }

    public User(Guid number)
    {
        Number = number;
    }

    public User(
        Guid number,
        string name,
        string img)
    {
        Number = number;
        Name = name;
        Img = img;
    }
}
