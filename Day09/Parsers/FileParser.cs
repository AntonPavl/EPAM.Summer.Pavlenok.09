using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Parsers
{
    public static class FileParser
    {
        /// <summary>
        /// Search the word frequency in the text
        /// </summary>
        /// <param name="filePath">path to the file</param>
        /// <param name="word">Search word</param>
        /// <returns>number of coincidences</returns>
        public static int WordFrequency(string filePath, string word)
        {
            if (!File.Exists(filePath)) throw new FileNotFoundException();

            string[] buffer = File.ReadAllText(filePath).Split(' ', ',');
            return buffer.Count(x => x == word);
        }
        /// <summary>
        /// Search all words frequency in the text
        /// </summary>
        /// <param name="filePath">Path to the file</param>
        /// <returns>Dictionary with ([Word] - Value) pairs</returns>
        public static Dictionary<string,int> WordFrequency(string filePath)
        {
            if (!File.Exists(filePath)) throw new FileNotFoundException();
            Dictionary<string, int> ret = new Dictionary<string, int>();
            string[] buffer = File.ReadAllText(filePath).Split(' ', ',');
            for (int i = 0; i < buffer.Length; i++)
            {
                if (ret.ContainsKey(buffer[i])) ret[buffer[i]]++;
                else
                    ret.Add(buffer[i], 1);
            }
            return ret;
        }

        public static void CreateTestFile()
        {
            var stream = File.Create("temp.txt");
            stream.Dispose();
            Random rand = new Random();
            for (int i = 0; i < 20000; i++)
            {
                File.AppendAllText("temp.txt"," " + rand.Next(200).ToString());

            }
        }
    }
}
