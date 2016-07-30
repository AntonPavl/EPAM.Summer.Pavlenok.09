using Library;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace BookListService
{
    public class XmlBookListStorage
    {
        private string filepath;
        /// <summary>
        /// create book Storage
        /// </summary>
        /// <param name="filePath"></param>
        public XmlBookListStorage(string filePath)
        {
            filepath = filePath;
            if (!File.Exists(filePath)) File.Create(filePath);
        }

        /// <summary>
        /// Load books from xml
        /// </summary>
        /// <returns></returns>
        public List<Book> LoadBooks()
        {
            List<Book> ret = new List<Book>();
            try
            {
                XDocument xDoc = XDocument.Load(filepath);
                var t = xDoc.Descendants("Book");
                foreach (var item in t)
                {
                    ret.Add( new Book(
                             item.Attribute("Author").Value,
                             item.Attribute("Title").Value,
                             Int32.Parse(item.Attribute("Id").Value),
                             Int32.Parse(item.Attribute("NumberOfPages").Value)   
                           )   
                   );
                }
            }
            catch (IOException ex)
            {
                throw ex;
            }

            return ret;
        }

        /// <summary>
        /// Save books to xml
        /// </summary>
        /// <param name="books"></param>
        public void SaveBooks(IEnumerable<Book> books)
        {
            if (ReferenceEquals(books, null)) throw new ArgumentNullException();

            XDocument xDoc = new XDocument();
                xDoc = new XDocument(
                new XElement("Books", books.Select(e =>
                     new XElement("Book",
                         new XAttribute("Id", e.Id),
                         new XAttribute("Title", e.Title),
                         new XAttribute("Author", e.Author),
                         new XAttribute("NumberOfPages", e.NumberOfPages))))
                );
            try
            {
                xDoc.Save(filepath);
            }
            catch (IOException ex)
            {
                throw ex;
            }
        }
    }
}
