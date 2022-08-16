namespace jiraF.User.API.Domain;

public class UserModel
{
    public Guid Number { get; }
    public DateTime DateOfRegistration { get; }
    public string Name { get; }

    public UserModel(
        string name)
    {
        Name = name;
    }

    public UserModel(
        Guid number,
        DateTime dateOfCreate,
        string name)
    {
        Number = number;
        DateOfRegistration = dateOfCreate;
        Name = name;
    }
}
