using Genocs.CleanArchitecture.Template.Domain.Exceptions;

namespace Genocs.CleanArchitecture.Template.Domain.ValueObjects;

public sealed class PositiveMoney : IEquatable<PositiveMoney>
{
    private readonly Money _value;

    private PositiveMoney()
    {
        _value = new Money(0);
    }

    public PositiveMoney(decimal value)
    {
        if (value < 0)
            throw new MoneyShouldBePositiveException("The 'Amount' should be positive.");

        _value = new Money(value);
    }

    public override bool Equals(object? obj)
    {
        if (obj is null)
        {
            return false;
        }

        if (ReferenceEquals(this, obj))
        {
            return true;
        }

        if (obj is decimal @decimal)
        {
            return @decimal == _value.ToDecimal();
        }

        return ((PositiveMoney)obj)._value == _value;
    }

    public Money ToMoney()
    {
        return _value;
    }

    internal PositiveMoney Add(PositiveMoney positiveAmount)
    {
        return _value.Add(positiveAmount._value);
    }

    public override int GetHashCode()
    {
        unchecked
        {
            int hash = 17;
            hash = (hash * 23) + _value.GetHashCode();
            return hash;
        }
    }

    internal Money Subtract(PositiveMoney positiveAmount)
    {
        return _value.Subtract(positiveAmount._value);
    }

    public bool Equals(PositiveMoney? other)
    {
        if (other is null) return false;
        return _value == other._value;
    }
}