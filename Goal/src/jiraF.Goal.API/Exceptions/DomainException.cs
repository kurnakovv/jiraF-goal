namespace jiraF.Goal.API.Exceptions;

public class DomainException : Exception
{
    public DomainException(string message): base(message) { }
}
