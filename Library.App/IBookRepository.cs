using Library.App;

public interface IBookRepository
{
    public void SaveBook(Book book);

    public Book? FindBook(string title);

    public List<Book> GetAllBooks();

    public List<Book> GetAvailableBooks();

}
