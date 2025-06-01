namespace Library.App
{
    public class Book
    {
        public string Title { get; }
        public bool IsBorrowed { get; private set; }

        public Book(string title)
        {
            Title = title;
            IsBorrowed = false;
        }

        public void Borrow() => IsBorrowed = true;

        public void Return() => IsBorrowed = false;

        public override string ToString()
        {
            return $"Title = {Title}, isBorrowed={IsBorrowed}";
        }
    }

}
