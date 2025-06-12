namespace Library.App.Tests;

public class BookServiceTests
{

    [Fact]
    public void Should_AddBook()
    {
        var mockBookRepository = new Mock<IBookRepository>();
        var service = new BookService(mockBookRepository.Object);
        service.AddBook("C# in Depth");
        // mockBookRepository.Verify(repo => repo.SaveBook(It.IsAny<Book>()));
        mockBookRepository.Verify(repo => 
        repo.SaveBook(It.Is<Book>(b => b.Title == "C# in Depth")));
    }

    [Fact]
public void Should_BorrowBookSuccessfully()
{
    // Arrange
    var mockRepo = new Mock<IBookRepository>();
    var book = new Book("Clean Code");
    mockRepo.Setup(r => r.FindBook("Clean Code")).Returns(book);

    var service = new BookService(mockRepo.Object);

    // Act
    var result = service.BorrowBook("Clean Code");

    // Assert
    Assert.True(result);
    Assert.True(book.IsBorrowed);
    mockRepo.Verify(r => r.FindBook("Clean Code"), Times.Once);
}

[Fact]
public void Should_ReturnBookSuccessfully()
{
    // Arrange
    var mockRepo = new Mock<IBookRepository>();
    var book = new Book("Refactoring");
    book.Borrow(); // спочатку видаємо книгу

    mockRepo.Setup(r => r.FindBook("Refactoring")).Returns(book);

    var service = new BookService(mockRepo.Object);

    // Act
    var result = service.ReturnBook("Refactoring");

    // Assert
    Assert.True(result);
    Assert.False(book.IsBorrowed); // після повернення має бути false
    mockRepo.Verify(r => r.FindBook("Refactoring"), Times.Once);
}

[Fact]
public void Should_ReturnAvailableBooks()
{
    // Arrange
    var mockRepo = new Mock<IBookRepository>();

    var availableBook = new Book("Book A");
    var borrowedBook = new Book("Book B");
    borrowedBook.Borrow();

    var allBooks = new List<Book> { availableBook, borrowedBook };

    mockRepo.Setup(r => r.GetAllBooks()).Returns(allBooks);

    var service = new BookService(mockRepo.Object);

    // Act
    var result = service.GetAvailableBooks();

    // Assert
    Assert.Single(result);
    Assert.Equal("Book A", result[0].Title);
    mockRepo.Verify(r => r.GetAllBooks(), Times.Once);
}



    // TODO: implement Should_BorrowBookSuccessfully
    // TODO: implement Should_ReturnBookSuccessfully
    // TODO: implement Should_ReturnAvailableBooks
}
