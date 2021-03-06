﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Library;
using System.IO;
using NLog;

namespace BookListService
{
    public class BookListServices
    {
        #region comparator
        private class Comparer : IComparer<Book>
        {
            public int Compare(Book a, Book b)
            {
                if (a == b) return 0;
                if (a == null) return -1;
                if (b == null) return 1;
                return a.Title.CompareTo(b.Title);
            }
        }
        private static IComparer<Book> Default = new Comparer();
        #endregion

        public List<Book> Books { get;  }
        /// <summary>
        /// create book list service
        /// </summary>
        /// <param name="books">List of books</param>
        public BookListServices(List<Book> books)
        {
            Books = books;
        }

        /// <summary>
        /// Add book to List
        /// </summary>
        /// <param name="book">Add book</param>
        public void AddBook(Book book)
        {
            if (!Books.Contains(book)) Books.Add(book);

        }

        /// <summary>
        /// Remove book from list
        /// </summary>
        /// <param name="book">Remove books</param>
        public void RemoveBook(Book book)
        {
            if (Books.Contains(book)) Books.Remove(book);
        }

        /// <summary>
        /// Find book by criterion
        /// </summary>
        /// <param name="tag">criterion</param>
        /// <returns></returns>
        public Book FindBookByTag(Func<Book,bool> tag)
        {
            return Books.First(tag);
        }

        /// <summary>
        /// Sort books by criterion
        /// </summary>
        /// <param name="comparator">criteron</param>
        public void SortBooksByTag(IComparer<Book> comparator = null)
        {
            if (comparator == null) comparator = Default;
            Books.Sort(comparator);
        }

        public void Save(IBookListStorage ibls)
        {
            if (ReferenceEquals(ibls, null)) throw new ArgumentNullException();
            ibls.SaveBooks(Books);
        }
    }

}
