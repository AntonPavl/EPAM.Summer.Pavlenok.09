using Library;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookListService
{
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
