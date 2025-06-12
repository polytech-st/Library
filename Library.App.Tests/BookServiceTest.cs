namespace Library.App.Tests;

public class BookServiceTests
{
    [Fact]
    public void Should_AddBook()
    {
        var mockBookRepository = new Mock<IBookRepository>();
        var service = new BookService(mockBookRepository.Object);
        service.AddBook("C# in Depth");

        mockBookRepository.Verify(repo => repo.SaveBook(new Book("C# in Depth")));
    }

    [Fact]
    public void Should_BorrowBookSuccessfully()
    {
        // Arrange
        var title = "Clean Code";
        var book = new Book(title);
        var mockRepo = new Mock<IBookRepository>();
        mockRepo.Setup(repo => repo.FindBook(title)).Returns(book);

        var service = new BookService(mockRepo.Object);

        // Act
        var result = service.BorrowBook(title);

        // Assert
        Assert.True(result);
        Assert.True(book.IsBorrowed);
        mockRepo.Verify(repo => repo.SaveBook(book), Times.Once);
    }

    [Fact]
    public void Should_ReturnBookSuccessfully()
    {
        // Arrange
        var title = "The Pragmatic Programmer";
        var book = new Book(title);
        book.Borrow(); // вручну позначаємо як позичену
        var mockRepo = new Mock<IBookRepository>();
        mockRepo.Setup(repo => repo.FindBook(title)).Returns(book);

        var service = new BookService(mockRepo.Object);

        // Act
        var result = service.ReturnBook(title);

        // Assert
        Assert.True(result);
        Assert.False(book.IsBorrowed);
        mockRepo.Verify(repo => repo.SaveBook(book), Times.Once);
    }

    [Fact]
    public void Should_ReturnAvailableBooks()
    {
        // Arrange
        var availableBooks = new List<Book>
        {
            new Book("Book A"),
            new Book("Book B")
        };
        var mockRepo = new Mock<IBookRepository>();
        mockRepo.Setup(repo => repo.GetAvailableBooks()).Returns(availableBooks);

        var service = new BookService(mockRepo.Object);

        // Act
        var result = service.GetAvailableBooks();

        // Assert
        Assert.Equal(2, result.Count);
        Assert.Contains(result, b => b.Title == "Book A");
        Assert.Contains(result, b => b.Title == "Book B");
    }
}
