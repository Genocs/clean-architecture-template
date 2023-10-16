namespace Genocs.CleanArchitecture.Template.Domain.ValueObjects;

public sealed class Money : IEquatable<Money>
{
    private readonly decimal _money;

    private Money()
    {
    }

    public Money(decimal value)
    {
        _money = value;
    }

    public decimal ToDecimal()
    {
        return _money;
    }

    public override bool Equals(object? obj)
    {
        if (obj is null) return false;

        if (ReferenceEquals(null, obj))
        {
            return false;
        }

        if (ReferenceEquals(this, obj))
        {
            return true;
        }

        if (obj is decimal)
        {
            return (decimal)obj == _money;
        }

        return ((Money)obj)._money == _money;
    }

    public override int GetHashCode()
    {
        unchecked
        {
            int hash = 17;
            hash = (hash * 23) + _money.GetHashCode();
            return hash;
        }
    }

    internal bool LessThan(PositiveMoney amount)
    {
        return _money < amount.ToMoney()._money;
    }

    internal bool IsZero()
    {
        return _money == 0;
    }

    public bool Equals(Money? other)
    {
        if (other is null) return false;
        return _money == other._money;
    }

    internal PositiveMoney Add(Money value)
    {
        return new PositiveMoney(_money + value.ToDecimal());
    }

    internal Money Subtract(Money value)
    {
        return new Money(_money - value.ToDecimal());
    }
}