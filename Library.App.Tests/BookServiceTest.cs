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
        mockBookRepository.Verify(repo => repo.SaveBook(new Book("C# in Depth")));
    }

    [Fact]
    public void Should_BorrowBookSuccessfully()
    {
        // Arrange
        var mockBookRepository = new Mock<IBookRepository>();
        var service = new BookService(mockBookRepository.Object);

        var book = new Book("C# in Depth");
        mockBookRepository.Setup(repo => repo.FindBook("C# in Depth")).Returns(book);

        // Act
        var result = service.BorrowBook("C# in Depth");

        // Assert
        Assert.True(result);
        Assert.True(book.IsBorrowed); 

        mockBookRepository.Verify(repo => repo.SaveBook(It.Is<Book>(b => b.Title == "C# in Depth" && b.IsBorrowed)), Times.Once); 
    }

    [Fact]
    public void Should_ReturnBookSuccessfully()
    {
        // Arrange
        var mockBookRepository = new Mock<IBookRepository>();
        var service = new BookService(mockBookRepository.Object);

        var book = new Book("C# in Depth");
        book.Borrow(); 

        mockBookRepository.Setup(repo => repo.FindBook("C# in Depth")).Returns(book);

        // Act
        var result = service.ReturnBook("C# in Depth");

        // Assert
        Assert.True(result);
        Assert.False(book.IsBorrowed); 

        mockBookRepository.Verify(repo => repo.SaveBook(It.Is<Book>(b => b.Title == "C# in Depth" && !b.IsBorrowed)), Times.Once);
    }


    [Fact]
    public void Should_ReturnAvailableBooks()
    {
        // Arrange
        var mockBookRepository = new Mock<IBookRepository>();
        var service = new BookService(mockBookRepository.Object);

        var book1 = new Book("C# in Depth"); 
        var book2 = new Book("Clean Code");
        book2.Borrow();
        var book3 = new Book("Design Patterns"); 

        var books = new List<Book> { book1, book2, book3 };

        mockBookRepository.Setup(repo => repo.GetAvailableBooks()).Returns(books.Where(b => !b.IsBorrowed).ToList());

        // Act
        var availableBooks = service.GetAvailableBooks();

        // Assert
        Assert.Contains(availableBooks, b => b.Title == "C# in Depth");
        Assert.Contains(availableBooks, b => b.Title == "Design Patterns");
        Assert.DoesNotContain(availableBooks, b => b.Title == "Clean Code");
    }
}
