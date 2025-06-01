using Library.App;

var bookService = new BookService();
bookService.AddBook("1814");
bookService.GetAvailableBooks().ForEach(Console.WriteLine);
