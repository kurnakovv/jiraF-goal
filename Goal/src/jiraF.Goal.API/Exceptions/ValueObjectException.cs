using System.Runtime.Serialization;

namespace jiraF.Goal.API.Exceptions;

[Serializable]
public class ValueObjectException : Exception
{
    public ValueObjectException(string message): base(message) { }

    protected ValueObjectException(SerializationInfo info, StreamingContext context) { }

    public override void GetObjectData(SerializationInfo info, StreamingContext context)
    {
        base.GetObjectData(info, context);
    }
}
