using System.Reflection;

namespace PLUG.System.Common.Domain;

public abstract class Enumeration : IEquatable<Enumeration>, IComparable<Enumeration>
{
    protected Enumeration()
    {
        this.Value = default;
        this.DisplayName = string.Empty;
    }

    protected Enumeration(int value, string displayName)
    {
        this.Value = value;
        this.DisplayName = displayName;
    }

    public int Value { get; }

    public string DisplayName { get; }

    public int CompareTo(Enumeration? other)
    {
        return other == null ? 1 : this.Value.CompareTo(other.Value);
    }


    public bool Equals(Enumeration? other)
    {
        if (other == null)
        {
            return false;
        }

        var typeMatches = this.GetType().Equals(other.GetType());
        var valueMatches = this.Value.Equals(other.Value);

        return typeMatches && valueMatches;
    }


    public static IEnumerable<T> GetAll<T>() where T : Enumeration, new()
    {
        var type = typeof(T);
        var fields = type.GetFields(BindingFlags.Public
                                    | BindingFlags.Static
                                    | BindingFlags.DeclaredOnly);

        foreach (var info in fields)
        {
            var instance = new T();
            var locatedValue = info.GetValue(instance) as T;

            if (locatedValue != null)
            {
                yield return locatedValue;
            }
        }
    }

    public static T FromValue<T>(int value) where T : Enumeration, new()
    {
        var matchingItem = Parse<T, int>(value, "value", item => item.Value == value);
        return matchingItem;
    }

    public static T FromDisplayName<T>(string displayName) where T : Enumeration, new()
    {
        var matchingItem = Parse<T, string>(displayName, "display name", item => item.DisplayName == displayName);
        return matchingItem;
    }

    private static T Parse<T, K>(K value, string description, Func<T, bool> predicate) where T : Enumeration, new()
    {
        var matchingItem = GetAll<T>().FirstOrDefault(predicate);

        if (matchingItem == null)
        {
            var message = string.Format("'{0}' is not a valid {1} in {2}", value, description, typeof(T));
            throw new ApplicationException(message);
        }

        return matchingItem;
    }

    public override string ToString()
    {
        return this.DisplayName;
    }
}