using System.Runtime.Serialization;

namespace jiraF.Goal.API.Exceptions;

[Serializable]
public class DomainException : Exception
{
    public DomainException(string message): base(message) { }

    protected DomainException(SerializationInfo info, StreamingContext context) : base(info, context) { }

    public override void GetObjectData(SerializationInfo info, StreamingContext context)
    {
        base.GetObjectData(info, context);
    }
}
