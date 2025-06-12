namespace Library.App
{
    public class BookService
    {
        private readonly List<Book> _books = new();

        public void AddBook(string title) => _books.Add(new Book(title));

        public Book? FindBook(string title) => _books.FirstOrDefault(b => b.Title == title);

        public List<Book> GetAllBooks() => [.. _books];

        public List<Book> GetAvailableBooks() => _books.Where(book => !book.IsBorrowed).ToList();

        public bool BorrowBook(string title)
        {
            var book = _books.FirstOrDefault(b => b.Title == title && !b.IsBorrowed);

            if (book != null)
            {
                book.Borrow();
                return true;
            }
            return false;
        }

        public bool ReturnBook(string title)
        {
            var book = _books.FirstOrDefault(b => b.Title == title && b.IsBorrowed);

            if (book != null)
            {
                book.Return();
                return true;
            }
            return false;
        }
    }

}
