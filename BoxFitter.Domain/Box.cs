namespace BoxFitter.Domain;

public record Box
{
    public BoxSize Size { get; set; } = BoxSize.Four;
    public List<Book> PackedBooks { get; set; } = new List<Book>();
}

public enum BoxSize
{
    Four,
    Six,
    Eight
}