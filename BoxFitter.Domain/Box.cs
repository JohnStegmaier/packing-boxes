namespace BoxFitter.Domain;

public class Box
{
    public BoxSize Size { get; set; } = BoxSize.Four;
    public List<Book> PackedBooks { get; set; } = new List<Book>();
    
    public override bool Equals(Object obj)
    {
        if (obj == null || !(obj is Box))
            return false;
        else
            return Equals((Box)obj);
    }

    protected bool Equals(Box other)
    {
        return Size == other.Size && PackedBooks.SequenceEqual(other.PackedBooks);
    }
}

public enum BoxSize
{
    Four,
    Six,
    Eight
}