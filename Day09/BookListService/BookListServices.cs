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
        public BookListServices(List<Book> books)
        {
            Books = books;
        }

        public void AddBook(Book book)
        {
            if (!Books.Contains(book)) Books.Add(book);
        }
        public void RemoveBook(Book book)
        {
            if (Books.Contains(book)) Books.Remove(book);
        }
        public Book FindBookByTag(Func<Book,bool> tag)
        {
            return Books.First(tag);
        }
        public void SortBooksByTag(IComparer<Book> comparator = null)
        {
            if (comparator == null) comparator = Default;
            Books.Sort(comparator);
        }
    }

    public class BinaryBookListStorage : IBookListStorage
    {
        private string filepath;
        public BinaryBookListStorage(string fileName)
        {
            filepath = fileName;
            //if (!File.Exists(fileName)) File.Create(fileName);
        }
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
