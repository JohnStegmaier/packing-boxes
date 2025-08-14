namespace BoxFitter.Domain;

public record Box
{
    public BoxSize Size { get; set; } = BoxSize.Four;
}

public enum BoxSize
{
    Four,
    Six,
    Eight
}