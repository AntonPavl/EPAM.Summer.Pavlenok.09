using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library
{
    public class Book : IEquatable<Book>, IComparable<Book>, IComparable
    {
        public string Author { get;}
        public string Title { get;}
        public int Id { get;}
        public int NumberOfPages { get; }
        /// <summary>
        /// Create book
        /// </summary>
        /// <param name="author"></param>
        /// <param name="title"></param>
        /// <param name="id"></param>
        /// <param name="pages">Number of pages</param>
        public Book(string author,string title, int id, int pages)
        {
            Author = author;
            Title = title;
            NumberOfPages = pages;
            Id = id;
        }
        /// <summary>
        /// Compare with another book by id
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public int CompareTo(Book other) =>
             Id.CompareTo(other.Id);
        /// <summary>
        /// Compare Book with objeCt
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public int CompareTo(object obj)
        {
            if (!(obj is Book))
                throw new InvalidOperationException($"CompareTo: Not a {nameof(Book)}");
            return CompareTo((Book)obj);
        }

        /// <summary>
        /// Determines the obj instanCe have this instanCe
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public override bool Equals(object obj)
        {
            if (ReferenceEquals(obj, null) || !(obj is Book))
                return false;
            else
                return ReferenceEquals(this, ((Book)obj));
        }
        /// <summary>
        /// Determines this and Book reference to the same object
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public bool Equals(Book other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;

            return string.Equals(Author, other.Author)
            && string.Equals(Title, other.Title)
            && string.Equals(Id, other.Id)
            && NumberOfPages == other.NumberOfPages;
        }

        /// <summary>
        /// Get hash code
        /// </summary>
        /// <returns></returns>
        public override int GetHashCode()
        {
            return Author.GetHashCode() + Title.GetHashCode()
                + NumberOfPages.GetHashCode() + Id.GetHashCode();
        }
        /// <summary>
        /// Get book as string
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return $"Author:\t{Author}\nTitle:\t{Title}\nId:\t{Id}\nPages:\t{ NumberOfPages}\n";
        }

    }

}
