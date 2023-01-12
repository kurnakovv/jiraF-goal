namespace jiraF.Goal.API.Exceptions;

[Serializable]
public class DomainException : Exception
{
    public DomainException(string message): base(message) { }
}
