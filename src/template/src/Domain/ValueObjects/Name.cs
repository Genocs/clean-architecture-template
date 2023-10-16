using Genocs.CleanArchitecture.Template.Domain.Exceptions;

namespace Genocs.CleanArchitecture.Template.Domain.ValueObjects;

public sealed class Name : IEquatable<Name>
{
    private readonly string _text;

    private Name() { }

    public Name(string text)
    {
        if (string.IsNullOrWhiteSpace(text))
            throw new NameShouldNotBeEmptyException("The 'Name' field is required");

        _text = text;
    }

    public override string ToString()
    {
        return _text;
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

        if (obj is string)
        {
            return obj.ToString() == _text;
        }

        return ((Name)obj)._text == _text;
    }

    public override int GetHashCode()
    {
        unchecked
        {
            int hash = 17;
            hash = (hash * 23) + _text.GetHashCode();
            return hash;
        }
    }

    public bool Equals(Name? other)
    {
        if (other is null) return false;
        return _text == other._text;
    }
}