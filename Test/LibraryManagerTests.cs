using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using LibrarySystem;

namespace LibrarySystem.Tests
{
    [TestFixture]
    public class LibraryManagerTests
    {
        private LibraryManager _manager;

        [SetUp]
        public void Setup()
        {
            _manager = new LibraryManager();
        }

        [Test]
        public void TC_AddBook_Success()
        {
            var book = new Book { Title = "Unit Testing", ISBN = "123", Author = "Author" };
            _manager.AddBook(book);
            Assert.AreEqual(1, _manager.GetAllBooks().Count);
        }

        [Test]
        public void TC_AddBook_DuplicateISBN_ThrowsException()
        {
            _manager.AddBook(new Book { Title = "Original", ISBN = "111" });
            Assert.Throws<InvalidOperationException>(() => _manager.AddBook(new Book { Title = "Duplicate", ISBN = "111" }));
        }

        [Test]
        public void TC_BorrowBook_AlreadyBorrowed_ThrowsException()
        {
            _manager.AddBook(new Book { Title = "The Hobbit", ISBN = "333" });
            _manager.BorrowBook("333");
            Assert.Throws<InvalidOperationException>(() => _manager.BorrowBook("333"));
        }

        [Test]
        public void TC_RemoveBook_Success()
        {
            _manager.AddBook(new Book { Title = "Delete Me", ISBN = "999" });
            bool result = _manager.RemoveBook("999");
            Assert.IsTrue(result);
            Assert.AreEqual(0, _manager.GetAllBooks().Count);
        }

        // 1. Fail because the book doesn't exist
        [Test]
        public void TC_BorrowBook_NonExistentISBN_ShouldThrowKeyNotFound()
        {
            // We try to borrow an ISBN that was never added
            Assert.Throws<KeyNotFoundException>(() => _manager.BorrowBook("999-NON-EXISTENT"));
        }

        // 2. Fail because returning a book that was never borrowed/added
        [Test]
        public void TC_ReturnBook_InvalidBook_ShouldThrowKeyNotFound()
        {
            Assert.Throws<KeyNotFoundException>(() => _manager.ReturnBook("000-INVALID"));
        }

        // 3. Fail because the ISBN is empty (Data Validation)
        [Test]
        public void TC_AddBook_EmptyISBN_ShouldThrowArgumentException()
        {
            var badBook = new Book { Title = "Invalid Book", ISBN = "" };
            Assert.Throws<ArgumentException>(() => _manager.AddBook(badBook));
        }
    }
}