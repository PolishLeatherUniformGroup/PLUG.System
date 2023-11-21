namespace PLUG.System.Common.Domain;

public abstract class ValueObject : IEquatable<ValueObject>
{
    public virtual bool Equals(ValueObject? other)
    {
        if (other != null)
        {
            return GetEqualityComponents()
                .SequenceEqual(other.GetEqualityComponents());
        }

        return false;
    }

    protected abstract IEnumerable<object> GetEqualityComponents();
}