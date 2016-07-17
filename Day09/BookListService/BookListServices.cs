using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Library;
using System.IO;

namespace BookListService
{
    public class BookListServices
    {
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
    }

    public class BinaryBookListStorage : IBookListStorage
    {
        private string filepath;
        /// <summary>
        /// create book Storage
        /// </summary>
        /// <param name="fileName"></param>
        public BinaryBookListStorage(string fileName)
        {
            filepath = fileName;
            //if (!File.Exists(fileName)) File.Create(fileName);
        }
        /// <summary>
        /// Load books from file
        /// </summary>
        /// <returns></returns>
        public List<Book> LoadBooks()
        {
            List<Book> ret = new List<Book>();
            using (BinaryReader reader = new BinaryReader(File.Open(filepath, FileMode.Open)))
            {
                while (reader.PeekChar() > -1)
                { 
                Book temp = new Book(
                    reader.ReadString(),
                    reader.ReadString(),
                    reader.ReadInt32(),
                    reader.ReadInt32());
                ret.Add(temp);
                 }
            }
            return ret;
        }
        /// <summary>
        /// Save books to file
        /// </summary>
        /// <param name="books"></param>
        public void SaveBooks(IEnumerable<Book> books)
        {
            using (BinaryWriter writer = new BinaryWriter(File.Open(filepath, FileMode.Create)))
            {
                foreach (var book in books)
                {
                    writer.Write(book.Author);
                    writer.Write(book.Title);
                    writer.Write(book.Id);
                    writer.Write(book.NumberOfPages);
                }
            }
        }
    }
}
