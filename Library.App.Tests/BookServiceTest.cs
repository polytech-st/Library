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

    [Fact]
    public void Should_BorrowBookSuccessfully()
    {
        var service = new BookService();
        service.AddBook("C# in Depth");

        var borrowSuccess = service.BorrowBook("C# in Depth");

        Assert.True(borrowSuccess);
        var book = service.FindBook("C# in Depth");
        Assert.NotNull(book);
        Assert.True(book!.IsBorrowed);
    }

    [Fact]
    public void Should_ReturnBookSuccessfully()
    {
        var service = new BookService();
        service.AddBook("C# in Depth");
        service.BorrowBook("C# in Depth");

        var returnSuccess = service.ReturnBook("C# in Depth");

        Assert.True(returnSuccess);
        var book = service.FindBook("C# in Depth");
        Assert.NotNull(book);
        Assert.False(book!.IsBorrowed);
    }

    [Fact]
    public void Should_ReturnAvailableBooks()
    {
        var service = new BookService();
        service.AddBook("C# in Depth");
        service.AddBook("Clean Code");

        service.BorrowBook("C# in Depth");

        var availableBooks = service.GetAvailableBooks();

        Assert.Single(availableBooks);
        Assert.Equal("C# in Depth", availableBooks[0].Title);
    }

}
