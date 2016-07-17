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
        public Book(string author,string title, int id, int pages)
        {
            Author = author;
            Title = title;
            NumberOfPages = pages;
            Id = id;
        }
        public int CompareTo(Book other) =>
             Id.CompareTo(other.Id);
        
        public int CompareTo(object obj)
        {
            if (!(obj is Book))
                throw new InvalidOperationException($"CompareTo: Not a {nameof(Book)}");
            return CompareTo((Book)obj);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(obj, null) || !(obj is Book))
                return false;
            else
                return ReferenceEquals(this, ((Book)obj));
        }
        public bool Equals(Book other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;

            return string.Equals(Author, other.Author)
            && string.Equals(Title, other.Title)
            && string.Equals(Id, other.Id)
            && NumberOfPages == other.NumberOfPages;
        }

        public override int GetHashCode()
        {
            return Author.GetHashCode() + Title.GetHashCode()
                + NumberOfPages.GetHashCode() + Id.GetHashCode();
        }

        public override string ToString()
        {
            return $"Author:\t{Author}\nTitle:\t{Title}\nId:\t{Id}\nPages:\t{ NumberOfPages}\n";
        }

    }

}
