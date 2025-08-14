namespace BoxFitter.Domain;

public interface IBoxFitter
{
    public PackingList PackBoxes(List<Book> booksToBePacked);
}