using System;
using NUnit.Framework;
using Parsers;
using System.Diagnostics;
using System.IO;

namespace FileParserNTest
{
    [TestFixture]
    public class FileParserTests
    {
        //[Test]
        //public void createTestFile()
        //{
        //    FileParser.CreateTestFile();
        //}

        [Test]
        public void WordFrequency_value100_result110()
        {
            int ret = FileParser.WordFrequency("temp.txt", "100");
            Assert.AreEqual(ret, 122);
        }

        [Test]
        public void WordFrequency_valueASD_result0()
        {
            int ret = FileParser.WordFrequency("temp.txt", "ASD");
            Assert.AreEqual(ret, 0);
        }

        [Test]
        public void WordFrequency_Dictionary_value120_result112()
        {
            var ret = FileParser.WordFrequency("temp.txt");
            Assert.AreEqual(ret["120"],112);
        }

        [Test]
        [ExpectedException(typeof(FileNotFoundException))]
        public void WordFrequency_FileNotFountExep()
        {
            int ret = FileParser.WordFrequency("tem13p.txt", "ASD");
        }


    }
}