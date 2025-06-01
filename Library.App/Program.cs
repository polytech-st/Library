using Library.App;
IBookRepository bookRepository = null;
var bookService = new BookService(bookRepository);
bookService.AddBook("1814");
bookService.GetAvailableBooks().ForEach(Console.WriteLine);
