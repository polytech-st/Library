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

    // TODO: implement Should_BorrowBookSuccessfully
    // TODO: implement Should_ReturnBookSuccessfully
    // TODO: implement Should_ReturnAvailableBooks
}
