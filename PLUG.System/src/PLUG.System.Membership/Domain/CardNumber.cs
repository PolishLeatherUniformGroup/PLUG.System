using PLUG.System.Common.Domain;

namespace PLUG.System.Membership.Domain;

public sealed class CardNumber : ValueObject
{
    public const string DefaultPrefix = "PLUG";
    public string Prefix { get; private set; }
    public int Number { get; private set; }

    public override string ToString()
    {
        return $"{this.Prefix}-{this.Number:0000}";
    }

    public CardNumber(int number, string prefix = DefaultPrefix)
    {
        this.Prefix = prefix;
        this.Number = number;
    }
    
    public static implicit operator string(CardNumber cardNumber) => cardNumber.ToString();
    public static explicit operator CardNumber(int number) => new (number);
    


    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return this.Prefix;
        yield return this.Number;
    }
}