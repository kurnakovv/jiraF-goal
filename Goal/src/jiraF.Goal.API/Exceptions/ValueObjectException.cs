namespace jiraF.Goal.API.Exceptions;

public class ValueObjectException : Exception
{
    public ValueObjectException(string message): base(message) { }
}
