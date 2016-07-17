using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;

namespace Library
{
    public static class BookExtension
    {
        public static Book[] SortBy(this Book[] arr,IComparer<Book> comparator)
        {
            if (arr == null)
                throw new ArgumentNullException();
            if (comparator == null)
                comparator = Comparer<Book>.Default;
            Array.Sort(arr, comparator);
            return null;
        }

    }
}
