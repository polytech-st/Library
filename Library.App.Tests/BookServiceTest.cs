namespace Library.App.Tests;

public class BookServiceTests
{

    [Fact]
    public void Should_AddBook()
    {
        // Тест перевіряє, що при додаванні книги викликається метод SaveBook з правильною книгою.
        var mockBookRepository = new Mock<IBookRepository>();
        var service = new BookService(mockBookRepository.Object);
        service.AddBook("C# in Depth");
        mockBookRepository.Verify(repo => repo.SaveBook(new Book("C# in Depth")));
    }

    [Fact]
    public void Should_BorrowBookSuccessfully()
    {
        // Тест перевіряє, що книга успішно видається (IsBorrowed стає true),
        // і що SaveBook викликається один раз для збереження змін.
        var mockBookRepository = new Mock<IBookRepository>();
        var book = new Book("C# in Depth");
        mockBookRepository.Setup(r => r.FindBook("C# in Depth")).Returns(book);
        var service = new BookService(mockBookRepository.Object);

        var result = service.BorrowBook("C# in Depth");

        Assert.True(result); // Перевіряємо, що метод повертає true (успішно видано)
        Assert.True(book.IsBorrowed); // Перевіряємо, що книга позначена як видана
        mockBookRepository.Verify(r => r.SaveBook(book), Times.Once); // Перевіряємо, що SaveBook викликано один раз
    }

    [Fact]
    public void Should_ReturnBookSuccessfully()
    {
        // Тест перевіряє, що книга успішно повертається (IsBorrowed стає false),
        // і що SaveBook викликається один раз для збереження змін.
        var mockBookRepository = new Mock<IBookRepository>();
        var book = new Book("C# in Depth");
        book.Borrow(); // Робимо книгу виданою
        mockBookRepository.Setup(r => r.FindBook("C# in Depth")).Returns(book);
        var service = new BookService(mockBookRepository.Object);

        var result = service.ReturnBook("C# in Depth");

        Assert.True(result); // Перевіряємо, що метод повертає true (успішно повернуто)
        Assert.False(book.IsBorrowed); // Перевіряємо, що книга позначена як доступна
        mockBookRepository.Verify(r => r.SaveBook(book), Times.Once); // Перевіряємо, що SaveBook викликано один раз
    }

    [Fact]
    public void Should_ReturnAvailableBooks()
    {
        // Тест перевіряє, що повертається список доступних книг,
        // і що GetAvailableBooks викликається один раз.
        var mockBookRepository = new Mock<IBookRepository>();
        var availableBooks = new List<Book>
        {
            new Book("Book 1"),
            new Book("Book 2")
        };
        mockBookRepository.Setup(r => r.GetAvailableBooks()).Returns(availableBooks);
        var service = new BookService(mockBookRepository.Object);

        var result = service.GetAvailableBooks();

        Assert.Equal(availableBooks, result); // Перевіряємо, що повернутий список співпадає з очікуваним
        mockBookRepository.Verify(r => r.GetAvailableBooks(), Times.Once); // Перевіряємо, що GetAvailableBooks викликано один раз
    }
}
