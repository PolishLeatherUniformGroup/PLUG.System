using PLUG.System.Common.Domain;

namespace PLUG.System.SharedDomain;

public class Money : ValueObject
{
    public const string PLN = "PLN";
    public const string EUR = "EUR";

    public decimal Amount { get; private set; }
    public string Currency { get; private set; }

    public Money(decimal amount, string currency = PLN)
    {
        this.Amount = amount;
        this.Currency = currency ?? throw new ArgumentNullException(nameof(currency));
    }

    public static Money operator +(Money left, Money right)
    {
        if (left.Currency != right.Currency)
        {
            throw new ArithmeticException("Currency not match");
        }

        return new Money(left.Amount + right.Amount, left.Currency);
    }

    public static Money operator -(Money left, Money right)
    {
        if (left.Currency != right.Currency)
        {
            throw new ArithmeticException("Currency not match");
        }

        return new Money(left.Amount - right.Amount, left.Currency);
    }

    public static Money operator *(Money left, decimal amount)
    {
        return new Money(left.Amount * amount, left.Currency);
    }

    public static Money operator /(Money left, decimal amount)
    {
        return new Money(left.Amount / amount, left.Currency);
    }

    public static bool operator ==(Money? left, Money? right)
    {
        if (left is null && right is null)
        {
            return true;
        }

        if (left is null || right is null)
        {
            return false;
        }

        if (left.Currency != right.Currency)
        {
            throw new ArithmeticException("Currency not match");
        }

        return left.Amount == right.Amount;
    }

    public static bool operator !=(Money? left, Money? right)
    {
        return !(left == right);
    }

    public static bool operator >(Money? left, Money? right)
    {
        if (left is null || right is null)
        {
            return false;
        }

        if (left.Currency != right.Currency)
        {
            throw new ArithmeticException("Currency not match");
        }

        return left.Amount > right.Amount;
    }

    public static bool operator >=(Money? left, Money? right)
    {
        if (left is null || right is null)
        {
            return false;
        }

        return left > right || left == right;
    }

    public static bool operator <(Money? left, Money? right)
    {
        if (left != right && (left is null || right is null))
        {
            return false;
        }

        return !(left > right);
    }


    public static bool operator <=(Money? left, Money? right)
    {
        if (left is null || right is null)
        {
            return false;
        }

        return left < right || left == right;
    }

    public static Money operator -(Money left)
    {
        return new Money(-left.Amount, left.Currency);
    }

    public bool IsZero() => this.Amount == 0;

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return this.Amount;
        yield return this.Currency;
    }
}