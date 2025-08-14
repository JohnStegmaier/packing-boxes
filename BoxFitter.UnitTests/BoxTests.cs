using System.Diagnostics.CodeAnalysis;
using BoxFitter.Domain;
// ReSharper disable InconsistentNaming

namespace BoxFitter.UnitTests;
 

[SuppressMessage("Performance", "CA1859:Use concrete types when possible for improved performance")]
public class BoxTests
{
    private readonly IBoxFitter boxFitter = new SimpleBoxFitter();

    [Fact]
    public void ShouldNotPackAnyBoxesWhenNoBooksAreGiven()
    {
        var noBooks = new List<Book>();
        
        var packingList = boxFitter.PackBoxes(noBooks);
        
        Assert.Empty(packingList.BooksThatCannotBePacked);
        Assert.Empty(packingList.PackedBoxes);
    }
    
    [Theory]
    [InlineData(1, BoxSize.Four)]
    [InlineData(2, BoxSize.Four)]
    [InlineData(3, BoxSize.Four)]
    [InlineData(4, BoxSize.Four)]
    [InlineData(5, BoxSize.Six)]
    [InlineData(6, BoxSize.Six)]
    [InlineData(7, BoxSize.Eight)]
    [InlineData(8, BoxSize.Eight)]
    public void ShouldPackOneBoxWhenOneBookUnderTheSizeIsGiven(int bookHeight, BoxSize expectedBoxSize)
    {
        var packingList = boxFitter.PackBoxes(GetOneBookOfSize(bookHeight));

        ValidatePackingListContains(OnePackedBoxOfSize(expectedBoxSize), packingList);
    }

    private static void ValidatePackingListContains(PackingList expectedPackingList, PackingList packingList)
    {
        Assert.Equal(expectedPackingList.BooksThatCannotBePacked, packingList.BooksThatCannotBePacked);
        Assert.Equal(expectedPackingList.PackedBoxes, packingList.PackedBoxes);
    }

    [Fact]
    public void ShouldNotPackAnyBoxesWhenBookIsTooLarge()
    {
        var oversizedBook = GetOneBookOfSize(9);
        
        var packingList = boxFitter.PackBoxes(oversizedBook);

        var expectedPackingList = new PackingList(PackedBoxes: [], BooksThatCannotBePacked: oversizedBook);
        
        ValidatePackingListContains(expectedPackingList, packingList);
    }
    
    
    // [Fact]
    // public void ShouldPackTwoBoxesOfSizeEightWhenTwoBooksOfSizeSevenAreGiven() 
    //     ShouldPackTwoBoxesWhenBooksOverTheSizeIsGiven()
    // {
    //     var twoBooksLargerThanSizeSix = GetOneBookSmallerThanSizeFour();
    //     
    //     var packingList = boxFitter.PackBoxes(oneBookSmallerThanSizeFour);
    //
    //     Assert.Equal(OneBoxOfSizeFour(), packingList);
    // }

    private static PackingList OnePackedBoxOfSize(BoxSize boxSize)
    {
        List<Box> expectedPackingList = new()
        {
            new Box() {Size = boxSize}
        };
        return new PackingList(expectedPackingList, new List<Book>());
    }

    private static List<Book> GetOneBookOfSize(int bookHeight)
    {
        List<Book> oneBookSmallerThanSizeFour = new()
        {
            new Book() {Height = bookHeight}
        };
        return oneBookSmallerThanSizeFour;
    }


    //book 1 = 1inch
    // Four 1 inch books would be box size 4
    // Ten 1 inch books would be box size 4 and box size 6
    // One 7 inch book and One 3 inch book would be box size 8 and box size 4
    // One 7 inch full book two half size 1 inch books a full 3 inch book four half size 1/2 inch books
    /*
     *  {
     *      books[
     *      "Book 1"
     *      "Book 2"
     *      ]
     *  }
     */
}