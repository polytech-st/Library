namespace Library.App.Tests;

public class BookServiceTests
{
    [Fact]
    public void Should_AddBook()
    {
        // Arrange: створюємо мок-репозиторій і сервіс
        var mockBookRepository = new Mock<IBookRepository>();
        var service = new BookService(mockBookRepository.Object);

        // Act: додаємо книгу
        service.AddBook("C# in Depth");

        // Assert: перевіряємо, що SaveBook викликано з книгою з правильним Title
        mockBookRepository.Verify(repo => repo.SaveBook(It.Is<Book>(b => b.Title == "C# in Depth")), Times.Once);
    }

    [Fact]
    public void Should_BorrowBookSuccessfully()
    {
        // Arrange: створюємо мок-репозиторій, сервіс і книгу
        var mockBookRepository = new Mock<IBookRepository>();
        var service = new BookService(mockBookRepository.Object);
        var book = new Book("C# in Depth");
        mockBookRepository.Setup(repo => repo.FindBook("C# in Depth")).Returns(book);

        // Act: видаємо книгу
        var result = service.BorrowBook("C# in Depth");

        // Assert: перевіряємо, що книга видана і SaveBook викликано з правильною книгою
        Assert.True(result);
        Assert.True(book.IsBorrowed);
        mockBookRepository.Verify(repo => repo.SaveBook(It.Is<Book>(b => b.Title == "C# in Depth" && b.IsBorrowed)), Times.Once);
    }

    [Fact]
    public void Should_ReturnBookSuccessfully()
    {
        // Arrange: створюємо мок-репозиторій, сервіс і видану книгу
        var mockBookRepository = new Mock<IBookRepository>();
        var service = new BookService(mockBookRepository.Object);
        var book = new Book("C# in Depth");
        book.Borrow();
        mockBookRepository.Setup(repo => repo.FindBook("C# in Depth")).Returns(book);

        // Act: повертаємо книгу
        var result = service.ReturnBook("C# in Depth");

        // Assert: перевіряємо, що книга повернута і SaveBook викликано з правильною книгою
        Assert.True(result);
        Assert.False(book.IsBorrowed);
        mockBookRepository.Verify(repo => repo.SaveBook(It.Is<Book>(b => b.Title == "C# in Depth" && !b.IsBorrowed)), Times.Once);
    }

    [Fact]
    public void Should_ReturnAvailableBooks()
    {
        // Arrange: створюємо мок-репозиторій, сервіс і список книг
        var mockBookRepository = new Mock<IBookRepository>();
        var service = new BookService(mockBookRepository.Object);
        var book1 = new Book("C# in Depth");
        var book2 = new Book("C++ megatron");
        book2.Borrow();
        var book3 = new Book("CHINAZES");
        var books = new List<Book> { book1, book2, book3 };
        mockBookRepository.Setup(repo => repo.GetAvailableBooks()).Returns(books.Where(b => !b.IsBorrowed).ToList());

        // Act: отримуємо доступні книги
        var availableBooks = service.GetAvailableBooks();

        // Assert: перевіряємо, що повернуті лише доступні книги
        Assert.Contains(availableBooks, b => b.Title == "C# in Depth");
        Assert.Contains(availableBooks, b => b.Title == "CHINAZES");
        Assert.DoesNotContain(availableBooks, b => b.Title == "C++ megatron");
        mockBookRepository.Verify(repo => repo.GetAvailableBooks(), Times.Once);
    }
}
