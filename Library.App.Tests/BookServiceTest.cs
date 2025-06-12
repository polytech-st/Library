using Xunit;
namespace Library.App.Tests {

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
            service.AddBook("Clean Code");

            var success = service.BorrowBook("Clean Code");

            Assert.True(success);
            var book = service.FindBook("Clean Code");
            Assert.True(book!.IsBorrowed);
        }

        [Fact]
        public void Should_ReturnBookSuccessfully()
        {
            var service = new BookService();
            service.AddBook("Refactoring");
            service.BorrowBook("Refactoring");

            var success = service.ReturnBook("Refactoring");

            Assert.True(success);
            var book = service.FindBook("Refactoring");
            Assert.False(book!.IsBorrowed);
        }
        [Fact]
        public void Should_ReturnAvailableBooks()
        {
            var service = new BookService();
            service.AddBook("Book A");
            service.AddBook("Book B");
            service.BorrowBook("Book A");

            var availableBooks = service.GetAvailableBooks();

            Assert.Single(availableBooks);
            Assert.Equal("Book B", availableBooks[0].Title);
        }
}
    // TODO: implement Should_BorrowBookSuccessfully
    // TODO: implement Should_ReturnBookSuccessfully
    // TODO: implement Should_ReturnAvailableBooks
}
