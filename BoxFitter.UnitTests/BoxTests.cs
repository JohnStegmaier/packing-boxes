using System.Diagnostics.CodeAnalysis;
using BoxFitter.Domain;
// ReSharper disable InconsistentNaming

namespace BoxFitter.UnitTests;


[SuppressMessage("Performance", "CA1859:Use concrete types when possible for improved performance")]
public class BoxTests
{
    private const string TEST_SKU = "420.69";
    private const string TEST_BOOK_NAME = "TEST_BOOK";
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
        ValidateOneBoxOfSize(expectedBoxSize, packingList);
    }

    [Fact]
    public void ShouldNotPackAnyBoxesWhenBookIsTooLarge()
    {
        var oversizedBook = GetOneBookOfSize(9);

        var packingList = boxFitter.PackBoxes(oversizedBook);

        var expectedPackingList = new PackingList(PackedBoxes: [], BooksThatCannotBePacked: oversizedBook);

        ValidatePackingListContains(expectedPackingList, packingList);
    }

    [Fact]
    public void ShouldPackBookInBox()
    {
        var packingList = boxFitter.PackBoxes(GetOneBookWithAllDetails());
        
        
        VerifyFirstBoxContainsTheSameBooks(
            GetPackingListWithOneBoxOfSizeFourContainingOneBookWithAllDetails(), 
            packingList);
    }

    private static PackingList GetPackingListWithOneBoxOfSizeFourContainingOneBookWithAllDetails()
    {
        Box ExpectedBox = GetBoxOfSizeFourWithBooks(GetOneBookWithAllDetails());
        var expectedPackingList = GetPackingListWithBox(ExpectedBox);
        return expectedPackingList;
    }

    private static void VerifyFirstBoxContainsTheSameBooks(PackingList expectedPackingList, PackingList packingList)
    {
        Assert.Equal(expectedPackingList.PackedBoxes[0].PackedBooks[0] , packingList.PackedBoxes[0].PackedBooks[0]);
    }

    private static PackingList GetPackingListWithBox(Box ExpectedBox)
    {
        return new PackingList(
            PackedBoxes: 
            new List<Box> {ExpectedBox}, 
            BooksThatCannotBePacked: 
            new List<Book>()
        );
    }

    private static Box GetBoxOfSizeFourWithBooks(List<Book> ProvidedBook)
    {
        return new Box() {PackedBooks = ProvidedBook , Size = BoxSize.Four};
    }

    [Fact]
    public void ShouldPackTwoIdenticalItemsThatFitInToOneBox()
    {
        var packingList = boxFitter.PackBoxes(GetTwoIdenticalFullyPopulatedBooks());
        
        ValidatePackingListContains(
            GetExpectedPackingListWithABoxOfSizeFourAndTwoIdenticalBooksPackedInside(), 
            packingList);
    }

    private static PackingList GetExpectedPackingListWithABoxOfSizeFourAndTwoIdenticalBooksPackedInside()
    {
        Box ExpectedBox = GetBoxOfSizeFourWithBooks(GetTwoIdenticalFullyPopulatedBooks());

        var expectedPackingList = new PackingList(
            PackedBoxes: 
            new List<Box> {ExpectedBox}, 
            BooksThatCannotBePacked: 
            new List<Book>()
        );
        return expectedPackingList;
    }

    private static List<Book> GetTwoIdenticalFullyPopulatedBooks()
    {
        var twoIdenticalSizeBooks = new List<Book>
        {
            new Book() {Sku = TEST_SKU, Name = TEST_BOOK_NAME, Height = 2},
            new Book() {Sku = TEST_SKU, Name = TEST_BOOK_NAME, Height = 1},
        };
        return twoIdenticalSizeBooks;
    }

    private static List<Book> GetOneBookWithAllDetails()
    {
        
        var bookWithAllDetails = new List<Book> 
        {
            new Book() {Sku = TEST_SKU, Name = TEST_BOOK_NAME, Height = 4}
        };
        return bookWithAllDetails;
    }

    private static void ValidatePackingListContains(PackingList expectedPackingList, PackingList packingList)
    {
        Assert.Equal(expectedPackingList.BooksThatCannotBePacked, packingList.BooksThatCannotBePacked);
        Assert.Equal(expectedPackingList.PackedBoxes, packingList.PackedBoxes);
    }

    private void ValidateOneBoxOfSize(BoxSize expectedBoxSize, PackingList packingList)
    {
        Assert.Single(packingList.PackedBoxes);
        Assert.Equal(expectedBoxSize, packingList.PackedBoxes.First().Size);
    }

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
            new Book() {Sku = TEST_SKU, Name = TEST_BOOK_NAME, Height = bookHeight}
        };
        return oneBookSmallerThanSizeFour;
    }
}