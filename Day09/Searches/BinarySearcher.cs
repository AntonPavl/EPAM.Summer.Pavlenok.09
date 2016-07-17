using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Searches
{
    public static class BinarySearcher
    {
        public sealed class Comparer : IComparer
        {
            public static readonly Comparer Default = new Comparer();
            public Comparer()
            {

            }

            public int Compare(Object a, Object b)
            {
                if (a == b) return 0;
                if (a == null) return -1;
                if (b == null) return 1;

                IComparable ia = a as IComparable;
                if (ia != null) return ia.CompareTo(b);

                IComparable ib = b as IComparable;
                if (ib != null) return -ib.CompareTo(a);
                throw new ArgumentException("Argument_ImplementIComparable");
            }

        }

        public static int Search(Array array, object obj)
        {
            if (ReferenceEquals(array, null)) throw new ArgumentNullException();
            return BinarySearch(array, array.GetLowerBound(0), array.GetUpperBound(0)+1, obj,null);
        }

        public static int Search(Array array, object obj, IComparer comparer)
        {
            if (ReferenceEquals(array, null)) throw new ArgumentNullException();
            return BinarySearch(array, array.GetLowerBound(0), array.GetUpperBound(0) + 1, obj, comparer);
        }

        private static int BinarySearch(Array array, int index, int length, Object value, IComparer comparer)
        {
            if (array.Rank != 1) throw new RankException("Rank_MultiDimNotSupported");
            if (ReferenceEquals(comparer,null)) comparer = Comparer.Default;

            Object[] objArray = array as Object[];       
            if (objArray != null)
            {
                return Body(index, length, (f) => objArray[f], value, comparer);
            }
            else
            {
                return Body(index, length, (f) => array.GetValue(f), value, comparer);
            }
        }
        private static int Body(int index, int length, Func<int, object> @delegate, object value, IComparer comparer)
        {
            int lo = index;
            int hi = index + length - 1;
            while (lo <= hi)
            {
                int i = GetMedian(lo, hi);
                int c;

                try
                {
                    c = comparer.Compare(@delegate(i), value);
                }
                catch 
                {
                    throw new InvalidOperationException("InvalidOperation_IComparerFailed");
                }
                if (c == 0) return i;
                if (c < 0)  lo = i + 1;
                else        hi = i - 1;
            }
            return ~lo;
        }

        private static int GetMedian(int low, int hi)
        {
            if (low > hi) throw new ArgumentException();
            if (hi - low < 0) throw new ArgumentException("Length overflow");
            return low + ((hi - low) >> 1);
        }
    }
}
