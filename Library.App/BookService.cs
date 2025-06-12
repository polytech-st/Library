namespace Library.App
{
    public class BookService
    {
        private readonly IBookRepository _bookRepository;

        public BookService(IBookRepository bookRepository) => _bookRepository = bookRepository;

        public void AddBook(string title) => _bookRepository.SaveBook(new Book(title));

        public Book? FindBook(string title) => _bookRepository.FindBook(title);

        public List<Book> GetAllBooks() => _bookRepository.GetAllBooks();

       public List<Book> GetAvailableBooks() =>
    _bookRepository.GetAllBooks().Where(b => !b.IsBorrowed).ToList();


        public bool BorrowBook(string title)
        {
            var book = FindBook(title);

            if (book != null && !book.IsBorrowed)
            {
                book.Borrow();
                _bookRepository.SaveBook(book);
                return true;
            }
            return false;
        }

        public bool ReturnBook(string title)
        {
            var book = FindBook(title);

            if (book != null && book.IsBorrowed)
            {
                book.Return();
                _bookRepository.SaveBook(book);
                return true;
            }
            return false;
        }
    }

}
