using System.Runtime.Serialization;

namespace StringCalculator;

[Serializable]
public class NegativesNotAllowedException : Exception
{
    public NegativesNotAllowedException(IEnumerable<int> negatives) :
        base($"Negatives not allowed: {string.Join(", ", negatives)}")
    { }

    protected NegativesNotAllowedException(SerializationInfo info, StreamingContext context)
        : base(info, context)
    { }
}