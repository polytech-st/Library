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

  [Fact]
    public void Should_BorrowBookSuccessfully()
    {
        var service = new BookService();
        service.AddBook("Test Book");

        var result = service.BorrowBook("Test Book");

        var book = service.FindBook("Test Book");

        Assert.True(result);
        Assert.True(book!.IsBorrowed);
    }

    // TODO: implement Should_ReturnBookSuccessfully

   [Fact]
    public void Should_ReturnBookSuccessfully()
    {
        var service = new BookService();
        service.AddBook("Test Book");
        service.BorrowBook("Test Book");

        var result = service.ReturnBook("Test Book");

        var book = service.FindBook("Test Book");

        Assert.True(result);
        Assert.False(book!.IsBorrowed);
    }

    // TODO: implement Should_ReturnAvailableBooks

    [Fact]
    public void Should_ReturnAvailableBooks()
    {
        var service = new BookService();
        service.AddBook("Book 1");
        service.AddBook("Book 2");
        service.AddBook("Book 3");
        service.BorrowBook("Book 1");

        var availableBooks = service.GetAvailableBooks();

        Assert.Contains(availableBooks, b => b.Title == "Book 1");
        Assert.DoesNotContain(availableBooks, b => b.Title == "Book 2");
        Assert.DoesNotContain(availableBooks, b => b.Title == "Book 3");
    }
}
