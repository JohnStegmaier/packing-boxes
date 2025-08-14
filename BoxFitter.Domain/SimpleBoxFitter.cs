namespace BoxFitter.Domain;

public class SimpleBoxFitter : IBoxFitter
{
    public PackingList PackBoxes(List<Book> booksToBePacked)
    {
        
        List<Box> listOfPackedBoxes = new List<Box>();
        List<Book> booksThatCannotBePacked = new List<Book>();
        
        if (booksToBePacked.Count == 0)
        {
            return new PackingList(new List<Box>(), new List<Book>());
        }

        foreach (var book in booksToBePacked)
        {
            switch (book.Height)
            {
                case <= 4:
                    listOfPackedBoxes.Add(new Box() {Size = BoxSize.Four});
                    break;
                case <= 6:
                    listOfPackedBoxes.Add(new Box() {Size = BoxSize.Six});
                    break;
                case <= 8:
                    listOfPackedBoxes.Add(new Box() {Size = BoxSize.Eight});
                    break;
                default:
                    booksThatCannotBePacked.Add(book);
                    break;
            }
        }

        return new PackingList(listOfPackedBoxes, booksThatCannotBePacked);
    }
}