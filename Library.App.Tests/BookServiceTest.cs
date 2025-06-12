namespace Library.App.Tests;

public class BookServiceTests
{
    public readonly string cid = "C# in Depth";
    public readonly string tb = "Test Book";
    [Fact]
    public void Should_AddBook()
    {
        var service = new BookService();
        service.AddBook(cid);

        var book = service.FindBook(cid);

        Assert.NotNull(book);
        Assert.Equal(cid, book!.Title);
    }

    [Fact]
    public void Should_BorrowBookSuccessfully()
    {
        var service = new BookService();
        service.AddBook(tb);

        var result = service.BorrowBook(tb);

        var book = service.FindBook(tb);

        Assert.True(result);
        Assert.True(book!.IsBorrowed);
    }

    [Fact]
    public void Should_ReturnBookSuccessfully()
    {
        var service = new BookService();
        service.AddBook(tb);
        service.BorrowBook(tb);

        var result = service.ReturnBook(tb);

        var book = service.FindBook(tb);

        Assert.True(result);
        Assert.False(book!.IsBorrowed);
    }

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
