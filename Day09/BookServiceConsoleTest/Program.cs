using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookListService;
using Library;
using NLog;

namespace BookServiceConsoleTest
{
    class Program
    {
        private static Random random = new Random();
        public static string RandomString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, length)
                .Select(s => s[random.Next(s.Length)]).ToArray());
        }
        private class ComparerById : IComparer<Book>
        {
            public int Compare(Book x, Book y)
            {
                return -x.Id.CompareTo(y.Id);
            }
        }

        public static void Binary()
        {
            var bbls = new BinaryBookListStorage("temp.txt");
            List<Book> books = new List<Book>();
            for (int i = 0; i < 50; i++)
            {
                Book temp = new Book(RandomString(10), RandomString(10), i, i * 10);
                books.Add(temp);
            }
            Logger logger = LogManager.GetCurrentClassLogger();
            bbls.SaveBooks(books);
            books = bbls.LoadBooks();
            Console.WriteLine(books.Count);

            books.Add(new Book("TestAuthor", "TestTitle", 100, 100));
            bbls.SaveBooks(books);
            books = bbls.LoadBooks();
            Console.WriteLine(books.Count);

            var bls = new BookListServices(books);
            bls.AddBook(new Book("AddBook", "AddBookTitle", 100, 100));
            Console.WriteLine(books.Count);

            bls.RemoveBook(new Book("AddBook", "AddBookTitle", 100, 100));
            Console.WriteLine(books.Count);

            Book book = bls.FindBookByTag(x => x.Title == "TestTitle");
            Console.WriteLine(book.ToString());

            Console.WriteLine("BeforeSort 0 = " + books[0].Id);
            Console.WriteLine("BeforeSort 1 = " + books[1].Id);
            bls.SortBooksByTag(new ComparerById());
            Console.WriteLine("AfterSort 0 = " + books[0].Id);
            Console.WriteLine("AfterSort 1 = " + books[1].Id);
        }

        public static void Xml()
        {
            List<Book> books = new List<Book>();
            for (int i = 0; i < 10; i++)
            {
                Book temp = new Book(RandomString(10), RandomString(10), i, i * 10);
                books.Add(temp);
            }
            XmlBookListStorage xbls = new XmlBookListStorage(@"D:\2.xml");
            xbls.SaveBooks(books);
            books = null;
            books = xbls.LoadBooks();
            books = null;
        }
        static void Main(string[] args)
        {
            Xml();
           // Console.ReadKey();
        }
    }
}
