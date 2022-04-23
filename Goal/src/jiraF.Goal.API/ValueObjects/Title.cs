using jiraF.Goal.API.Exceptions;

namespace jiraF.Goal.API.ValueObjects;

public class Title
{
    public string Value { get; }

    public Title(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new ValueObjectException("Cannot set empty value.");
        if (value.Length <= 1 || value.Length >= 51)
            throw new ValueObjectException("Invalid length (availability 2 - 50)");

        Value = value;
    }
}
