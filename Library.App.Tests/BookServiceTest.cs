namespace Library.App.Tests;

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
    // TODO: implement Should_BorrowBookSuccessfully

    public void Should_BorrowBookSuccessfully()
    {
        var service = new BookService();
        service.AddBook("C# in Depth");

        var result = service.BorrowBook("C# in Depth");

        Assert.True(result);
        var book = service.FindBook("C# in Depth");
        Assert.True(book!.IsBorrowed);
    }
    //Додає книгу "C# in Depth".Позичає цю книгу, перевіряє, що метод повернув true (успішно позичено) перевіряє, що у книги властивість IsBorrowed стала true.
    [Fact]
    // TODO: implement Should_ReturnBookSuccessfully

    public void Should_ReturnBookSuccessfully()
    {
        var service = new BookService();
        service.AddBook("C# in Depth");
        service.BorrowBook("C# in Depth");

        var result = service.ReturnBook("C# in Depth");

        Assert.True(result);
        var book = service.FindBook("C# in Depth");
        Assert.False(book!.IsBorrowed);
    }

    //Додає книгу "C# in Depth".Позичає цю книгу.Повертає книгу.Перевіряє, що метод повернув true (успішно повернуто).
    //Перевіряє, що у книги властивість IsBorrowed стала false.

    [Fact]
    // TODO: implement Should_ReturnAvailableBooks

    public void Should_ReturnAvailableBooks()
    {
        var service = new BookService();
        service.AddBook("Book 1");
        service.AddBook("Book 2");
        service.AddBook("Book 3");
        service.BorrowBook("Book 2");

        var availableBooks = service.GetAvailableBooks();

        Assert.Contains(availableBooks, b => b.Title == "Book 2");
        Assert.DoesNotContain(availableBooks, b => b.Title == "Book 1");
        Assert.DoesNotContain(availableBooks, b => b.Title == "Book 3");
    }
}

//Додає три книги.Позичає "Book 2".Отримує список доступних книг.
//Перевіряє, що "Book 2" є у списку доступних (ймовірно, тут помилка: позичена книга не має бути доступною).
//Перевіряє, що "Book 1" і "Book 3" не входять у список доступних (ймовірно, тут теж логічна помилка).