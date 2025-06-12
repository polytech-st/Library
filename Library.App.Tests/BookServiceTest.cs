using Library.App;
using Moq;
using Xunit;

namespace Library.App.Tests
{
    public class BookServiceTests
    {
        [Fact]
        public void Should_AddBook()
        {
            var mockBookRepository = new Mock<IBookRepository>();
            var service = new BookService(mockBookRepository.Object);

            service.AddBook("C# in Depth");

            mockBookRepository.Verify(repo => repo.SaveBook(It.Is<Book>(b => b.Title == "C# in Depth" && !b.IsBorrowed)), Times.Once);
        }

        [Fact]
        public void Should_BorrowBookSuccessfully()
        {
            var book = new Book("Test Book");
            var mockRepo = new Mock<IBookRepository>();
            mockRepo.Setup(r => r.FindBook("Test Book")).Returns(book);

            var service = new BookService(mockRepo.Object);
            var result = service.BorrowBook("Test Book");

            Assert.True(result);
            Assert.True(book.IsBorrowed);
            mockRepo.Verify(r => r.SaveBook(It.Is<Book>(b => b.Title == "Test Book" && b.IsBorrowed)), Times.Once);
        }

        [Fact]
        public void Should_ReturnBookSuccessfully()
        {
            var book = new Book("Returnable Book");
            book.Borrow(); // робимо книгу вже позиченою
            var mockRepo = new Mock<IBookRepository>();
            mockRepo.Setup(r => r.FindBook("Returnable Book")).Returns(book);

            var service = new BookService(mockRepo.Object);
            var result = service.ReturnBook("Returnable Book");

            Assert.True(result);
            Assert.False(book.IsBorrowed);
            mockRepo.Verify(r => r.SaveBook(It.Is<Book>(b => b.Title == "Returnable Book" && !b.IsBorrowed)), Times.Once);
        }

        [Fact]
        public void Should_ReturnAvailableBooks()
        {
            var books = new List<Book>
            {
                new Book("Available Book 1"),
                new Book("Available Book 2")
            };

            var mockRepo = new Mock<IBookRepository>();
            mockRepo.Setup(r => r.GetAvailableBooks()).Returns(books);

            var service = new BookService(mockRepo.Object);
            var result = service.GetAvailableBooks();

            Assert.Equal(2, result.Count);
            Assert.Contains(result, b => b.Title == "Available Book 1");
            Assert.Contains(result, b => b.Title == "Available Book 2");
        }
    }
}
