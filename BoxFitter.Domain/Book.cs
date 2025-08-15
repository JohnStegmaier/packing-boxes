using System;
namespace BoxFitter.Domain;

public class Book
{
    public required string Sku { get; init; }
    public required string Name { get; init; }
    public required float Height { get; init; }
    
    public override bool Equals(Object obj)
    {
        if (obj == null || !(obj is Book))
            return false;
        else
            return Equals((Book)obj);
    }

    protected bool Equals(Book other)
    {
        return Sku == other.Sku && Name == other.Name && Height.Equals(other.Height);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(Sku, Name, Height);
    }
}