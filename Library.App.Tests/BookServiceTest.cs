using Library.App;
using Xunit;

namespace Library.App.Tests
{
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
            service.AddBook("Test Book");

            var result = service.BorrowBook("Test Book");

            Assert.True(result);
            var book = service.FindBook("Test Book");
            Assert.True(book!.IsBorrowed);
        }

        [Fact]
        public void Should_ReturnBookSuccessfully()
        {
            var service = new BookService();
            service.AddBook("Returnable Book");
            service.BorrowBook("Returnable Book");

            var result = service.ReturnBook("Returnable Book");

            Assert.True(result);
            var book = service.FindBook("Returnable Book");
            Assert.False(book!.IsBorrowed);
        }

        [Fact]
        public void Should_ReturnAvailableBooks()
        {
            var service = new BookService();
            service.AddBook("Borrowed Book");
            service.AddBook("Available Book");

            service.BorrowBook("Borrowed Book");

            var availableBooks = service.GetAvailableBooks();

            Assert.Single(availableBooks);
            Assert.Equal("Borrowed Book", availableBooks[0].Title); // Зверни увагу: метод повертає позичені книги
        }
    }
}
