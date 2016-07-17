using System;
using NUnit.Framework;
using Searches;
using System.Collections;

namespace BinarySearcherNTest
{
    [TestFixture]
    public class BinarySearcherTests
    {
        [Test]
        public void Search_intarray_value_2_result_2()
        {
            int[] array = { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
            int pos = BinarySearcher.Search(array, 2);
            Assert.AreEqual(pos, 2);
        }

        [Test]
        public void Search_stringarray_value_2_result_2()
        {
            string[] sarray = { "0", "1", "2", "3", "4", "5", "6", "7", "8", "9", "10" };
            int pos = BinarySearcher.Search(sarray, "2");
            Assert.AreEqual(pos, 2);
        }


        struct Temp : IComparable
        {
            public int ID;
            public Temp(int id)
            {
                ID = id; 
            }

            public int CompareTo(object obj)
            {
                if (!(obj is Temp)) throw new ArgumentException();
                return ID.CompareTo(((Temp)obj).ID);
            }

            public override string ToString()
            {
                return ID.ToString();
            }

        }

        public class Tempcomparer : IComparer
        {
            public int Compare(object a, object b)
            {
                if (ReferenceEquals(a, b)) return 0;
                if (ReferenceEquals(a, null)) return -1;
                if (ReferenceEquals(b, null)) return 1;
                if (((Temp)a).ID > (int)b) return 1;
                if (((Temp)a).ID < (int)b) return -1;
                return 0;
            }
        }


        [Test]
        public void Search_structID_value_10_result_3()
        {
            Temp[] tarray = new Temp[4];
            tarray[0] = new Temp(3);
            tarray[1] = new Temp(6);
            tarray[2] = new Temp(9);
            tarray[3] = new Temp(10);
            int pos = BinarySearcher.Search(tarray,10,new Tempcomparer());
            Assert.AreEqual(pos, 3);
        }

        [Test]
        public void Search_struct_struct_3()
        {
            Temp[] tarray = new Temp[4];
            tarray[0] = new Temp(3);
            tarray[1] = new Temp(6);
            tarray[2] = new Temp(9);
            tarray[3] = new Temp(10);
            Temp search = new Temp(10);
            int pos = BinarySearcher.Search(tarray, search);
            Assert.AreEqual(pos, 3);
        }

        [Test]
        [ExpectedException(typeof(RankException))]
        public void Search_RankExeption()
        {
            Temp[,] tarray = new Temp[4,2];
            BinarySearcher.Search(tarray, 2);
        }

        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Search_ArgumentNullExeption()
        {
            Temp[] tarray = null;
            BinarySearcher.Search(tarray, 2);
        }


        public class Wrongcomparer : IComparer
        {
            public int Compare(object a, object b)
            {
                if (ReferenceEquals(a, b)) return 0;
                if (ReferenceEquals(a, null)) return -1;
                if (ReferenceEquals(b, null)) return 1;
                if (((Temp)a).ID > ((Temp)b).ID) return 1;
                if (((Temp)a).ID < ((Temp)b).ID) return -1;
                return 0;
            }
        }


        [Test]
        [ExpectedException(typeof(InvalidOperationException))]
        public void Search_comparer_ArgumentExeption()
        {
            Temp[] tarray = new Temp[4];
            tarray[0] = new Temp(3);
            tarray[1] = new Temp(6);
            tarray[2] = new Temp(9);
            tarray[3] = new Temp(10);
            BinarySearcher.Search(tarray, 2, new Wrongcomparer());
        }
    }
}