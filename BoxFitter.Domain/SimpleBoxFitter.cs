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

        var boxToBePacked = new Box();
        
        foreach (var book in booksToBePacked)
        {
            switch (book.Height)
            {
                case <= 4:
                    boxToBePacked.Size = BoxSize.Four;
                    boxToBePacked.PackedBooks.Add(book);
                    break;
                case <= 6:
                    boxToBePacked.Size = BoxSize.Six;
                    boxToBePacked.PackedBooks.Add(book);
                    break;
                case <= 8:
                    boxToBePacked.Size = BoxSize.Eight;
                    boxToBePacked.PackedBooks.Add(book);
                    break;
                default:
                    booksThatCannotBePacked.Add(book);
                    break;
            }
        }

        if (boxToBePacked.PackedBooks.Count > 0)
        {
            listOfPackedBoxes.Add(boxToBePacked);            
        }
        return new PackingList(listOfPackedBoxes, booksThatCannotBePacked);
    }
}