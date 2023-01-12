namespace jiraF.Goal.API.Exceptions;

[Serializable]
public class ValueObjectException : Exception
{
    public ValueObjectException(string message): base(message) { }
}
