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
        // TODO: implement Should_BorrowBookSuccessfully

    public void Should_BorrowBookSuccessfully()
    {
        var service = new BookService();
        service.AddBook("CLR via C#");

        var result = service.BorrowBook("CLR via C#");

        Assert.True(result);
        var book = service.FindBook("CLR via C#");
        Assert.True(book!.IsBorrowed);
    }

    [Fact]
        // TODO: implement Should_ReturnBookSuccessfully

    public void Should_ReturnBookSuccessfully()
    {
        var service = new BookService();
        service.AddBook("Effective C#");
        service.BorrowBook("Effective C#");

        var result = service.ReturnBook("Effective C#");

        Assert.True(result);
        var book = service.FindBook("Effective C#");
        Assert.False(book!.IsBorrowed);
    }

    [Fact]
        // TODO: implement Should_ReturnAvailableBooks

    public void Should_ReturnAvailableBooks()
    {
        var service = new BookService();
        service.AddBook("Book 1");
        service.AddBook("Book 2");
        service.AddBook("Book 3");
        service.BorrowBook("Book 2");

        var availableBooks = service.GetAvailableBooks();

        Assert.Contains(availableBooks, b => b.Title == "Book 2");
        Assert.DoesNotContain(availableBooks, b => b.Title == "Book 1");
        Assert.DoesNotContain(availableBooks, b => b.Title == "Book 3");
    }
}