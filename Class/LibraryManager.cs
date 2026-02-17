using System;
using System.Collections.Generic;
using System.Linq;

namespace LibrarySystem
{
    public class Book
    {
        public string Title { get; set; } = string.Empty;
        public string Author { get; set; } = string.Empty;
        public string ISBN { get; set; } = string.Empty;
        public int Year { get; set; }
        public bool IsBorrowed { get; set; } = false;
    }

    public class LibraryManager
    {
        private readonly List<Book> _books = new List<Book>();

        public void AddBook(Book book)
        {
            if (string.IsNullOrWhiteSpace(book.ISBN)) throw new ArgumentException("ISBN is required.");
            if (_books.Any(b => b.ISBN == book.ISBN)) throw new InvalidOperationException("ISBN already exists.");
            _books.Add(book);
        }

        public bool RemoveBook(string isbn)
        {
            var book = _books.FirstOrDefault(b => b.ISBN == isbn);
            return book != null && _books.Remove(book);
        }

        public List<Book> SearchByTitle(string title)
        {
            return _books.Where(b => b.Title.Contains(title, StringComparison.OrdinalIgnoreCase)).ToList();
        }

        public List<Book> GetAllBooks() => _books.ToList();

        public bool IsAvailable(string isbn)
        {
            var book = _books.FirstOrDefault(b => b.ISBN == isbn);
            return book != null && !book.IsBorrowed;
        }

        public void BorrowBook(string isbn)
        {
            var book = _books.FirstOrDefault(b => b.ISBN == isbn);
            if (book == null) throw new KeyNotFoundException("Book not found.");
            if (book.IsBorrowed) throw new InvalidOperationException("Book is already borrowed.");
            book.IsBorrowed = true;
        }

        public void ReturnBook(string isbn)
        {
            var book = _books.FirstOrDefault(b => b.ISBN == isbn);
            if (book == null) throw new KeyNotFoundException("Book not found.");
            book.IsBorrowed = false;
        }
    }
}