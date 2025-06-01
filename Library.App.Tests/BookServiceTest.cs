namespace Library.App.Tests;

public class BookServiceTests
{

    [Fact]
    public void Should_AddBook()
    {
        var service = new BookService();
        service.AddBook("C# in Depth");

        var book = service.FindBook("C# in Depth");

        Assert.NotNull(book);
        Assert.Equal("C# in Depth", book!.Title);
    }

    // TODO: implement Should_BorrowBookSuccessfully
    // TODO: implement Should_ReturnBookSuccessfully
    // TODO: implement Should_ReturnAvailableBooks
}
